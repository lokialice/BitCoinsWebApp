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

namespace BitCoinsWebApp.Controllers
{
    public class AccountController : BaseController
    {

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
            UserProfile user = _userService.Login(userAccount.UserName, userAccount.Password, true);
            if (user != null)
            {
                Session["UserLogin"] = user.UserName;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Fuck you";
                return View("Index");
            }
        }

        [AllowAnonymous]
        public ActionResult Register(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        [HttpPost]
        public ActionResult Register(UserProfile userAccount)
        {
            if (_userService.CheckEmailExist(userAccount.Email) == null
                && _userService.CheckUserName(userAccount.UserName) == null)
            {
                if (userAccount.Password.Equals(userAccount.ConfirmPassword))
                {
                    var user = _userService.Create(userAccount);
                    if (user)
                    {
                        return View("Login");
                    }
                    else
                    {
                        return View("Index");
                    }
                }
                else
                {
                    ViewBag.PasswordError = "Password not match ! Please try again!";
                    return View();
                }
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

    }
}
