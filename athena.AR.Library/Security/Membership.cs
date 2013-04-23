using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using athena.AR.Data;
using athena.AR.Data.Artifacts;
using athena.AR.Library.Interface;

namespace athena.AR.Library.Security
{
    public sealed class Membership : IMembership
    {
      
      public  Status CreateUser(string userName, string password, string email, DateTime createDate, bool status)
        {

            try
            {
                int ret = 0;
                using (var context = DataObjectEntity.GetObjectContext())
                {
                    var queryExistingUser = context.USERs.Where(u => u.USERNAME.Equals(userName, StringComparison.CurrentCultureIgnoreCase));
                    if (queryExistingUser.Any())
                        return Status.DuplicateUserName;

                    var newUser = new USER
                        {
                            USERNAME = userName,
                            PASSWORD = Encoding.ASCII.GetString(Cryptography.EncryptPassword(password)),
                            EMAIL = email,
                            CREATEDDATE = DateTime.Now,
                            ACTIVE = 0
                        };
                    //newUser.ACTIVE_OLD = new byte[] { 0 };
                    context.AddToUSERs(newUser);
                    ret = context.SaveChanges();
                }
                return ret > 0 ? Status.Succeed : Status.Fail;
            }
            catch (Exception ex)
            {
                Common.ExceptionManager.AppendAsync(ex);
                return Status.Fail;
            }
        }

      public Status ValidateUser(string username, string password)
        {
            try
            {
               using (var context = DataObjectEntity.GetObjectContext())
                {
                    var user = context.USERs.FirstOrDefault(u => u.USERNAME.ToLower() == username.ToLower());
                    if (user ==null)
                        return Status.InvalidUserName;
                    var encryptedPassword = Encoding.ASCII.GetString(Cryptography.EncryptPassword(password));
                    var validPassword = user.PASSWORD == encryptedPassword;
                    if (!validPassword)
                        return Status.InvalidPassword;

                    return user.ACTIVE == 1 ? Status.ValidUser : Status.InActiveUser;
                }
            }
            catch (Exception ex)
            {
                Common.ExceptionManager.AppendAsync(ex);
                return Status.Fail;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
      public User GetUser(string username)
        {
            try
            {
                using (var context = DataObjectEntity.GetObjectContext())
                {
                    var user = context.USERs.FirstOrDefault(u => u.USERNAME.ToLower() == username.ToLower());
                    if (user == null)
                        return null;
                    return new User() { UserId = user.USERID, UserName = user.USERNAME, Password = user.PASSWORD, EmailAdd = user.EMAIL, Fullname = user.USERNAME };

                }
            }
            catch (Exception ex)
            {
                Common.ExceptionManager.AppendAsync(ex);
                return null;
            }
        }

    }
}