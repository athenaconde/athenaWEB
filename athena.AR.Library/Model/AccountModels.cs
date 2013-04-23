using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;


namespace athena.AR.Library.Model
{

    public class ChangePassword
    {

        public virtual string OldPassword { get; set; }


        public virtual string NewPassword { get; set; }

        public virtual string ConfirmPassword { get; set; }
    }

    public class LogOnModel
    {

        public virtual string UserName { get; set; }


        public virtual string Password { get; set; }

        public virtual bool RememberMe { get; set; }
    }

    public class RegisterModel
    {

        public string UserName { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }


        public string FullName { get; set; }


        public string Email { get; set; }
    }
}
