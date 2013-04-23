using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace athena.AR.Library.Security
{
    public class User : IUser
    {

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string EmailAdd { get; set; }
        public DateTime DateCreated { get; set; }



        public Status ChangePassword(string userName, string password)
        {
            throw new NotImplementedException();
        }

        internal void SaveUserLog(action userAction,String userName, DateTime loginDateTime)
        {
            //using (var userManager = new Data.UserManager())
            //{
            //    if ( userAction== action.Login){
            //        var user = userManager.GetUserByUserName(userName);
            //        var newUserLog = new Data.USERLOG();
            //        newUserLog.USER = user; 
            //        newUserLog.DATETIMELOGIN = loginDateTime;
            //        userManager.AddToUserLog(newUserLog);
            //    }
            //    else if (userAction == action.Logout)
            //    {
            //        var userLatestLog = userManager.GetUserLastestLogin(userName);
            //        if (userLatestLog!=null)
            //        userLatestLog.DATETIMELOGOUT = loginDateTime;
            //    }
            //    //Save everything that are changed.
            //    userManager.Save();
            //}
        }

        public enum action
        {
            Login,
            Logout
        }
    }
}