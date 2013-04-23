using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using athena.AR.Library.Interface;

namespace athena.AR.Library.Security
{
    public interface IMembership
    {

      Status CreateUser(string userName, string password, string email, DateTime createDate, bool status);
      Status ValidateUser(string username, string password);
      User GetUser(string username);
    }
}
