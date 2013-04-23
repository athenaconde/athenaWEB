using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace athena.AR.Library.Security
{
    public interface IUser
    {
        Status ChangePassword(string userName, string password);
    }
}