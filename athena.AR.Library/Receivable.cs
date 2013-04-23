using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using athena.AR.Data;

namespace athena.AR.Library
{
    public sealed class Receivable
    {

        public Receivable() { }

        public IEnumerable<Model.ReceivableModel> GetList(int param, int startIndex, out int countTotal, out int countCurrent)
        {
            using (var context = DataObjectEntity.GetObjectContext())
            {

                var receivablesData = new List<Model.ReceivableModel>();
                foreach (var r in context.RECEIVABLEs.Where(r => param == 0 || r.ISPAID == (param == 1 ? 0 : 1)))
                {
                  receivablesData.Add(new Model.ReceivableModel()
                    {
                        //Id = EncryptionManager.EncryptId(r.ID),
                        Id = r.ID,
                        Sales = new Model.SalesModel()
                        {
                            Item = r.SALE.ITEM,
                            Qty = r.SALE.QTY,
                            UnitPrice = r.SALE.UNITPRICE,
                            TotalAmount = r.SALE.TOTALPRICE
                        },
                        Customer = new Model.CustomerModel()
                        {
                            Name = r.SALE.CREDITOR.NAME,
                            Id = r.SALE.CREDITOR.ID,
                            Address = r.SALE.CREDITOR.ADDRESS,
                            Email = r.SALE.CREDITOR.EMAIL,
                            Active = r.SALE.CREDITOR.ACTIVE == 1,
                            DateCreated = (DateTime)r.SALE.CREDITOR.DATECREATED,
                            LastDateModified = r.SALE.CREDITOR.LASTDATEMODIFIED
                        },
                        PaymentDue = r.AMOUNTDUE,
                        PaymentDueDate = r.DUEDATE,
                        Balance = (decimal)r.BALANCE,
                        LastPaidDate = !r.PAYMENTs.Any() ? DateTime.MinValue :
                                     r.PAYMENTs.OrderByDescending(p => p.PAYMENTDATE).First().PAYMENTDATE
                    });
                }

                countTotal = receivablesData.Count();
                countCurrent = receivablesData.Skip(startIndex).Take(10).Count();
                return receivablesData.Skip(startIndex).Take(10);
               
            }

        }
    }
}
