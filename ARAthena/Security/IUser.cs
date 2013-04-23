using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using athena.AR.Library.Security;

namespace athena.AR.Security
{
    public interface IUser
    {
        Status ChangePassword(string userName, string password);
    }
}