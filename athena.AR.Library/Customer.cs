using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using athena.AR.Data;
using athena.AR.Data.Artifacts;

namespace athena.AR.Library
{
    public class Customer
    {
        private string _userName;
        public Customer(string userName)
        {
            _userName = userName;
        }


        public int Create(string name, string address, string phone, string email, bool active)
        {
            using (var context = DataObjectEntity.GetObjectContext())
            {
                var newCustomer = new CREDITOR()
                {
                    NAME = name,
                    ADDRESS = address,
                    PHONE = phone,
                    EMAIL = email,
                    ACTIVE = active ? 1 : 0,
                    DATECREATED = DateTime.Now,//model.DateCreated always return the minvalue even it was been initialized on Get http request.
                    LASTDATEMODIFIED = DateTime.Now,
                    USER = context.USERs.FirstOrDefault(u => u.USERNAME.Equals(_userName, StringComparison.CurrentCultureIgnoreCase))
                };
                context.AddToCREDITORs(newCustomer);
                return context.SaveChanges();

            }
        }


        public IEnumerable<Model.CustomerModel> GetList(string param, int startIndex, out int countTotal, out int countCurrent)
        {
            using (var context = DataObjectEntity.GetObjectContext())
            {
                var _customers = from c in context.CREDITORs
                                 where (param.Equals("All", StringComparison.CurrentCultureIgnoreCase) ||
                                 c.ACTIVE == (param.Equals("Active", StringComparison.CurrentCultureIgnoreCase) ? 1 : 0))
                                 orderby c.NAME
                                 select new Model.CustomerModel()
                                 {
                                     Id = c.ID,
                                     Name = c.NAME,
                                     Address = c.ADDRESS,
                                     Phone = c.PHONE,
                                     Email = c.EMAIL,
                                     DateCreated = (DateTime)(c.DATECREATED ?? DateTime.MinValue),
                                     LastDateModified = c.LASTDATEMODIFIED,
                                     Active = (c.ACTIVE == 1 ? true : false)
                                 };
                countTotal = _customers.Count();
                countCurrent = _customers.Skip(startIndex).Take(10).Count();
                return _customers.Skip(startIndex).Take(10).ToList();

            }

        }
    }
}