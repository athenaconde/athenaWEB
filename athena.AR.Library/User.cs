using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using athena.AR.Data;
using athena.AR.Library.Common;
using athena.AR.Data.Artifacts;

namespace athena.AR.Library
{
   public  class User
    {


       public static void CreateUserLoginAsync(string userName)
       {
           Task.Factory.StartNew(() => CreateUserLogin(userName));
       }


       public static void CreateUserLogin(string userName)
       {
           try
           {
               using (var userEntity = new UserEntity())
               {
                   var newUserLog = new USERLOG
                       {
                           USER = userEntity.GetUserByUserName(userName),
                           DATETIMELOGIN = DateTime.Today
                       };
                   userEntity.AddToUserLog(newUserLog);
                   userEntity.Save();
               }
           }
           catch (Exception ex)
           {
               Common.ExceptionManager.AppendAsync(ex);
           }
       }

       public static void CreateUserLogoutAsync(string userName)
       {
           Task.Factory.StartNew(()=> CreateUserLogout(userName));
       }

       public static void CreateUserLogout(string userName)
       {
           try
           {
               using (var userEntity = new UserEntity())
               {
                   var userLog = userEntity.GetUserLastestLogin(userName);
                   userLog.DATETIMELOGOUT = DateTime.Today;
                   userEntity.Save();
               }
           }
           catch (Exception ex)
           {
               Common.ExceptionManager.AppendAsync(ex);
           }
       }
      
    }
}
