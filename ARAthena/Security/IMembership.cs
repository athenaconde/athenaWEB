using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using athena.AR.Models;
using athena.AR.Library.Security;

namespace athena.AR.Security
{
    public interface IMembership
    {

     Status CreateUser(RegisterModel model);
      Status ValidateUser(string username, string password);
      User GetUser(string username);
    }
}
