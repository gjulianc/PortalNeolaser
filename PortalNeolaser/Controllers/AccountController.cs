using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using WebMatrix.WebData;
using PortalNeolaser.Filters;
using PortalNeolaser.Models;
using PortalNeolaser.Helpers;

namespace PortalNeolaser.Controllers {
    [Authorize]
    [InitializeSimpleMembership]
    public class AccountController : Controller {

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login() {
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl) {
            if(ModelState.IsValid) {
                if(WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe.Value)) {

                    //Obtener el usuario y logica para redirigirle al area correspondiente
                    if (Roles.IsUserInRole(model.UserName, "Administrador"))
                    {
                        MvcApplication.Log.WriteLog(String.Format("{0};Acceso;{1};Acceso al sistema.", DateTime.Now, model.UserName));
                        return RedirectToAction("Index", "Auditoria", new { Area = "Admin" });
                    }
                    else if (Roles.IsUserInRole(model.UserName, "Mantenimiento"))
                    {
                        MvcApplication.Log.WriteLog(String.Format("{0};Acceso;{1};Acceso al sistema.", DateTime.Now, model.UserName));
                        return RedirectToAction("Index", "Sucursal", new { Area = "Mobile" });
                    }
                    else if (Roles.IsUserInRole(model.UserName, "Gestor"))
                    {
                        MvcApplication.Log.WriteLog(String.Format("{0};Acceso;{1};Acceso al sistema.", DateTime.Now, model.UserName));
                        return RedirectToAction("Index", "Sucursales", new { Area = "Client" });
                    }
                    else
                        return Redirect(returnUrl ?? "/");
                }
                ViewBag.ErrorMessage = "El nombre de usuario o contraseña introducido no es válido";
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff() {
            WebSecurity.Logout();
            return Redirect("/");
        }

        //
        // GET: /Account/Register

        [AllowAnonymous]
        public ActionResult Register() {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model) {
            if(ModelState.IsValid) {
                // Attempt to register the user
                try {
                    WebSecurity.CreateUserAndAccount(model.UserName, model.Password);
                    WebSecurity.Login(model.UserName, model.Password);
                    return Redirect("/");
                }
                catch(MembershipCreateUserException e) {
                    ViewBag.ErrorMessage = ErrorCodeToString(e.StatusCode);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePassword

        public ActionResult ChangePassword() {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordModel model) {
            if(ModelState.IsValid) {
                bool changePasswordSucceeded;
                try {
                    changePasswordSucceeded = WebSecurity.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);
                }
                catch(Exception) {
                    changePasswordSucceeded = false;
                }
                if(changePasswordSucceeded) {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else {
                    ViewBag.ErrorMessage = "La contraseña actual es inconrrecta o la nueva contraseña no es válida.";
                }
                
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess() {
            return View();
        }

 
        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus) {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch(createStatus) {
                case MembershipCreateStatus.DuplicateUserName:
                    return "El nombre de usuario ya existe. Por favor, introduzca un nombre de usuario diferente.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion

        public ActionResult Acceder()
        {
            if (User.Identity.IsAuthenticated == true)
            {
                if (Roles.IsUserInRole(User.Identity.Name, "Administrador"))
                    return RedirectToAction("Index", "Dashboard", new { Area = "Admin" });
                else if (Roles.IsUserInRole(User.Identity.Name, "Mantenimiento"))
                    return RedirectToAction("Index", "Sucursal", new { Area = "Mobile" });
                else if (Roles.IsUserInRole(User.Identity.Name, "Gestor"))
                    return RedirectToAction("Index", "Dashboard", new { Area = "Client" });
                else
                   return Redirect("/");

            }
            else
            {
                return RedirectToAction("Login");
            }
        }
    }
}