using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Threading.Tasks;
//using System.Web.Security;
using athena.AR.Models;
using athena.AR.Security;
using System.Web.Security;


namespace athena.AR.Controllers
{
    public class AccountController : Controller
    {

        private IMembership _membership;
        //
        // GET: /Account/LogOn

        public ActionResult LogOn()
        {
            return View();
        }

        private athena.AR.Security.IMembership Membership { get { return _membership ?? (_membership = new athena.AR.Security.Membership()); } }

        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            try
            {
                if (ModelState.IsValid) 
                {
                    var status = Membership.ValidateUser(model.UserName, model.Password);
                    if (status == athena.AR.Library.Security.Status.ValidUser)
                    {
                        var user = Membership.GetUser(model.UserName);
                        FormsAuthentication.SetAuthCookie(user.Fullname, model.RememberMe);

                        //Async Log into the database the user login datime and time.
                        //Task.Factory.StartNew(() =>
                        //                {
                        //                    var userSecurity = new Security.User();
                        //                    userSecurity.SaveUserLog(Security.User.action.Login,model.UserName, DateTime.Now);
                        //                }
                        //             );

                        Library.User.CreateUserLoginAsync(model.UserName);


                        if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                            && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                            //return RedirectToAction("Index", "AccountReceivable");
                        }

                      
                    }
                    else
                    {
                        ModelState.AddModelError("", ErrorCodeToString(status));
                    }
                }
            }
            catch (Exception ex) {
                Common.ExceptionLogger.AppendAsync(ex);              
            }
            // If we got this far, something failed, redisplay form
            return View(model);
           
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            //Async Log into the database the user login datime and time.
            //Task.Factory.StartNew(() =>
            //                        {
            //                            var userSecurity = new Security.User();
            //                            userSecurity.SaveUserLog(Security.User.action.Logout,User.Identity.Name, DateTime.Now);
            //                        }
            //                    );
            Library.User.CreateUserLogoutAsync(User.Identity.Name);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            try{
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                //Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);
                var status = Membership.CreateUser(model);

                if (status == athena.AR.Library.Security.Status.Succeed)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(status));
                }
            }
        }
         catch (Exception ex) {
                Common.ExceptionLogger.AppendAsync(ex);              
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePassword

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
             var status = athena.AR.Library.Security.Status.Succeed ;
                try
                {
                    //MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    athena.AR.Library.Security.User currentUser = Membership.GetUser(User.Identity.Name);
                    status = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                   
                }

                if (status== athena.AR.Library.Security.Status.Succeed)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        #region Status Codes
        private static string ErrorCodeToString(athena.AR.Library.Security.Status createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case athena.AR.Library.Security.Status.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case athena.AR.Library.Security.Status.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case athena.AR.Library.Security.Status.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case athena.AR.Library.Security.Status.InActiveUser:
                    return "The account is inactive. Please contact your administrator to activate the account.";

                case athena.AR.Library.Security.Status.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case athena.AR.Library.Security.Status.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case athena.AR.Library.Security.Status.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case athena.AR.Library.Security.Status.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case athena.AR.Library.Security.Status.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case athena.AR.Library.Security.Status.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion


    }
}
