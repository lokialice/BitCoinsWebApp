namespace BitCoinsWebApp.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Transactions;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;
    using DotNetOpenAuth.AspNet;
    using Microsoft.Web.WebPages.OAuth;
    using WebMatrix.WebData;
    using BitCoinsWebApp.Model;
    using BitCoinsWebApp.BLL;
    using log4net;
    using BitCoinsWebApp.Utilities;

    public class AccountController : BaseController
    {
        #region member
        private static readonly ILog _log = LogManager.GetLogger(typeof(AccountController).Name);

        #endregion

        #region public member

        #endregion

        #region method
        /// <summary>
        /// Logins the specified return URL.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>ActionResult.</returns>
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        /// <summary>
        /// Logins the specified user account.
        /// </summary>
        /// <param name="userAccount">The user account.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        public ActionResult Login(UserProfile userAccount)
        {
            UserProfile user = _userService.Login(userAccount.UserName, userAccount.Password);
            if (user != null)
            {
                Session["UserLogin"] = user.UserName;
                return RedirectToAction("Index", "Manage");
            }
            else
            {
                ViewBag.Error = "Username or Password invalid ! Please try again !";
                return View("Login");
            }
        }

        /// <summary>
        /// Registers the specified return URL.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>ActionResult.</returns>
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }


        /// <summary>
        /// Registers the specified user account.
        /// </summary>
        /// <param name="userAccount">The user account.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        public ActionResult Register(UserProfile userAccount, string id)
        {
            if (_userService.CheckEmailExist(userAccount.Email) == null
                && _userService.CheckUserName(userAccount.UserName) == null)
            {
                if (!String.IsNullOrEmpty(id))
                {
                    var userParent = _userService.GetUserByUserName(id);
                    userAccount.IDParent = userParent.ID;
                    userAccount.Password = "12345678@Ab";
                }
                else 
                {
                    userAccount.IDParent = ConfigurationManagerKey.IDParent;
                    userAccount.Password = "12345678@Ab";
                }
                var user = _userService.Create(userAccount);              
                return View("Login");
                           
            }
            else
            {
                if (_userService.CheckEmailExist(userAccount.Email) != null)
                {
                    ViewBag.EmailError = "Email already exist ! Please try again!";
                    return View();
                }
                else
                {
                    if (_userService.CheckUserName(userAccount.UserName) != null)
                    {
                        ViewBag.UserNameError = "User Name already exist ! Please try again!";
                        return View();
                    }
                }
                return View();
            }


        }
        /// <summary>
        /// Profiles this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        [SessionExpire]
        public ActionResult Profile()
        {
            return View("Manage", UserCurrent);
        }

        /// <summary>
        /// Updates the profile.
        /// </summary>
        /// <param name="userAccounts">The user accounts.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        [SessionExpire]
        public ActionResult UpdateProfile(UserProfile userAccounts)
        {
            userAccounts.ID = UserCurrent.ID;
            _userService.Update(userAccounts);
            return View("Manage", UserCurrent);
        }

        /// <summary>
        /// Logs the out.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Remove("UserLogin");
            return RedirectToAction("Index", "Home");
        }       
        #endregion

    }
}
