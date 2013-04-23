using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using athena.AR.Library.Model;
using athena.AR.Data;

namespace athena.AR.Library
{
    public sealed class Sales
    {

        private string _userName;

        public Sales(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentNullException("userName");
            _userName = userName;
        }

        public void Create(int customerID, string itemName, int quantity, decimal unitPrice, DateTime salesDate, decimal monthlyInterest, int monthsToPay)
        {
            using (var context = DataObjectEntity.GetObjectContext())
            {
                var customer = from c in context.CREDITORs.Where(creditor => creditor.ID == customerID)
                               select new CustomerModel()
                               {
                                   Id = c.ID,
                                   Name = c.NAME,
                                   Address = c.ADDRESS,
                                   Phone = c.PHONE,
                                   Email = c.EMAIL,
                                   Active = c.ACTIVE == 1,
                                   DateCreated = (DateTime)c.DATECREATED,
                                   LastDateModified = c.LASTDATEMODIFIED
                               };
                Create(customer.FirstOrDefault(), itemName, quantity, unitPrice, salesDate, monthlyInterest, monthsToPay);


            }
        }

        public void Create(CustomerModel customer, string itemName, int quantity, decimal unitPrice, DateTime salesDate, decimal monthlyInterest, int monthsToPay)
        {
            using (var context = DataObjectEntity.GetObjectContext())
            {
                var user = context.USERs.FirstOrDefault(u => u.USERNAME.Equals(_userName, StringComparison.CurrentCultureIgnoreCase));
                var newSales = new athena.AR.Data.Artifacts.SALE()
                {
                    ITEM = itemName,
                    QTY = quantity,
                    UNITPRICE = unitPrice,
                    TOTALPRICE = quantity * unitPrice,
                    SALESDATE = salesDate,
                    CREDITOR = context.CREDITORs.FirstOrDefault(c => c.ID == customer.Id),
                    LASTMODIFIEDDATE = DateTime.Now,
                    USER = user
                };
                context.AddToSALES(newSales);
                if (context.SaveChanges() > 0)
                {
                    var firstOrDefault = context.SALES.Where(s => s.LASTMODIFIEDBY == user.USERID).OrderByDescending(s => s.ID).FirstOrDefault();
                    if (firstOrDefault != null)
                    {
                        var interest = new athena.AR.Data.Artifacts.RECEIVABLEINTEREST()
                            {
                                SALESID = firstOrDefault.ID,
                                MONTHLYINTEREST = monthlyInterest,
                                MONTHSTOPAY = monthsToPay,
                                USER = user,
                                LASTMODIFIEDDATE = DateTime.Now

                            };
                        context.AddToRECEIVABLEINTERESTs(interest);
                    }
                    context.SaveChanges();
                }
            }
        }

        public void Delete()
        { }

        public void Edit()
        { }

        public IEnumerable<SalesModel> GetList(int startIndex, out int totalCount, out int totalCurrent)
        {
            using (var context = DataObjectEntity.GetObjectContext())
            {
                var salesData = from s in context.SALES.Include("CREDITOR")
                                orderby s.ID ascending
                                select new SalesModel()
                                  {
                                      Id = s.ID,
                                      Item = s.ITEM,
                                      Qty = s.QTY,
                                      UnitPrice = s.UNITPRICE,
                                      TotalAmount = s.TOTALPRICE,
                                      SalesDate = s.SALESDATE,
                                      Customer = new CustomerModel()
                                      {
                                          Name = s.CREDITOR.NAME,
                                          Address = s.CREDITOR.ADDRESS,
                                          Active = s.CREDITOR.ACTIVE == 1
                                      }
                                  };
                totalCount = salesData.Count();
                totalCurrent = salesData.Skip(startIndex).Take(10).Count();
                return salesData.Skip(startIndex).Take(10).ToList();
            }
        }
    }
}
