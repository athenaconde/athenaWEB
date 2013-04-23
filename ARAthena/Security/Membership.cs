using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using athena.AR.Library.Security;
namespace athena.AR.Security
{
    internal sealed class Membership : IMembership
    {
        private athena.AR.Library.Security.Membership membership;

        public Membership()
        {
            membership = new athena.AR.Library.Security.Membership();
        }
 
      public  Status CreateUser(Models.RegisterModel model)
        {
            return membership.CreateUser(model.UserName, model.Password, model.Email, DateTime.Today, false);
        }

      public Status ValidateUser(string username, string password)
        {
            //return Status.ValidUser; // for testing 
            return membership.ValidateUser(username, password);         
        }

      public User GetUser(string username)
        {
            return membership.GetUser(username);
        }
    }
}