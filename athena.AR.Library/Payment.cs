using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using athena.AR.Data;

namespace athena.AR.Library
{
  public sealed  class Payment
    {
      private string _userName;

      public Payment(string userName)
      {
          if (string.IsNullOrWhiteSpace(userName))
              throw new ArgumentNullException("userName");
          _userName = userName;
      }

      public Model.PaymentModel New(string receivableID)
      {
          using (var context = DataObjectEntity.GetObjectContext())
          {
              //int ArId = EncryptionManager.DecryptStringId(Id);
              int ArId = Convert.ToInt32(receivableID);
              var ar = context.RECEIVABLEs.FirstOrDefault(a => a.ID == ArId);
              var paymntQuery = context.PAYMENTs.Where(p => p.RECEIVABLEID == ArId).OrderByDescending(a => a.PAYMENTDATE);
              athena.AR.Library.Model.ReceivableModel receivable = null;
              if (ar != null)
                  receivable = new athena.AR.Library.Model.ReceivableModel()
                  {
                      Id = ArId,
                      Customer = new athena.AR.Library.Model.CustomerModel(),
                      PaymentDue = ar.AMOUNTDUE,
                      PaymentDueDate = ar.DUEDATE,
                      Balance = (decimal)ar.BALANCE,
                      LastPaidDate = !paymntQuery.Any() ? DateTime.MinValue : Convert.ToDateTime(paymntQuery.First().PAYMENTDATE)
                  };
              var payment = new Model.PaymentModel()
              {
                  AccountReceivable = receivable,
                  PaymentDate = DateTime.Now
              };
              return payment;
          }
            
      }

      public void CreatePayment(string receivableID, decimal payAmount, DateTime paymentDate)
      {
          var ARId = Convert.ToInt32(receivableID); //Temporary fix. Still searching for a better approach on the UI side.
          CreatePayment(ARId, payAmount, paymentDate);
      }

      public void CreatePayment(int receivableID, decimal payAmount, DateTime paymentDate)
      {
          var receivable = new athena.AR.Library.Model.ReceivableModel();
          receivable.Id = receivableID;
          CreatePayment(receivable, payAmount, paymentDate);
      }

      public void CreatePayment(athena.AR.Library.Model.ReceivableModel receivable, decimal payAmount, DateTime paymentDate)
      {
         using (var context = DataObjectEntity.GetObjectContext())
          {
              // var ARId = EncryptionManager.DecryptStringId(Id);

              var ar = context.RECEIVABLEs.Include("SALE").FirstOrDefault(a => a.ID == receivable.Id);
              //var payments = context.PAYMENTs.Where(p => p.RECEIVABLEID==ARId);
              ar.AMOUNTDUE = ar.AMOUNTDUE - payAmount; //Added 01/11/2013
              ar.BALANCE = (ar.BALANCE - payAmount);
              ar.ISPAID = (ar.BALANCE <= 0 ? 1 : 0);

              var newPayment = new athena.AR.Data.Artifacts.PAYMENT()
              {
                  RECEIVABLE = ar,
                  AMOUNT = payAmount,
                  PAYMENTDATE = paymentDate,
                  USER = context.USERs.FirstOrDefault(u => u.USERNAME.Equals(_userName, StringComparison.CurrentCultureIgnoreCase)),
                  LASTMODIFIEDDATE = DateTime.Now
              };
              context.AddToPAYMENTs(newPayment);
              context.SaveChanges();

          }

      }

      public IEnumerable<Model.PaymentModel> GetPaymentHistory(int startIndex, out int countTotal, out int countCurrent)
      {
          using (var context = DataObjectEntity.GetObjectContext())
          {
              var paymentsData = from s in context.PAYMENTs.Include("RECEIVABLE")
                                 orderby s.ID ascending
                                 select new athena.AR.Library.Model.PaymentModel()
                                 {
                                     AccountReceivable = new athena.AR.Library.Model.ReceivableModel()
                                     {
                                         Id = s.RECEIVABLE.ID,
                                         Balance = (decimal)s.RECEIVABLE.BALANCE,
                                         PaymentDue = s.RECEIVABLE.AMOUNTDUE,
                                         PaymentDueDate = s.RECEIVABLE.DUEDATE
                                     },
                                     PayAmount = s.AMOUNT,
                                     PaymentDate = s.PAYMENTDATE
                                 };

              countTotal = paymentsData.Count();
              countCurrent = paymentsData.Skip(startIndex).Take(10).Count();
              return paymentsData.Skip(startIndex).Take(10).ToList();

          }
      }

    }
}
