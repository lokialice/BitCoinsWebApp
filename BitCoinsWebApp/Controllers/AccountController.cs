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
    using System.IO;

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
                }
                else
                {
                    userAccount.IDParent = ConfigurationManagerKey.IDParent;
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
        public ActionResult UpdateProfile(UserProfile userAccounts, HttpPostedFileBase uploadFile)
        {
            if (uploadFile!=null && uploadFile.ContentLength > 0)
            {
                int MaxContentLength = 1024 * 1024 * 3; //3 MB
                string[] AllowedFileExtensions = new string[] { ".jpg", ".gif", ".png" };

                if (!AllowedFileExtensions.Contains(uploadFile.FileName.Substring(uploadFile.FileName.LastIndexOf('.'))))
                {
                    ViewBag.Message = "Please file of type: " + string.Join(", ", AllowedFileExtensions);
                    return View("Manage", UserCurrent);
                }

                else if (uploadFile.ContentLength > MaxContentLength)
                {
                    ViewBag.Message = "Your file is too large, maximum allowed size is: " + MaxContentLength + " MB";
                    return View("Manage", UserCurrent);
                }
                else
                {
                    //TO:DO
                    var fileName = Path.GetFileName(uploadFile.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/Images"), fileName);
                    uploadFile.SaveAs(path);
                    ImageFileUpload img = new ImageFileUpload();
                    img.ImageFile = "~/Content/Images/" + fileName;
                    img.ImageName = fileName;
                    _imageService.Create(img);
                    userAccounts.ID = UserCurrent.ID;
                    userAccounts.ImageURL = _imageService.GetLasttestImg();
                    userAccounts.IsActive = UserCurrent.IsActive;
                    userAccounts.Amount = UserCurrent.Amount;
                    userAccounts.IDRole = UserCurrent.IDRole;
                    userAccounts.Age = UserCurrent.Age;
                    _userService.Update(userAccounts);                    
                    ModelState.Clear();
                    ViewBag.Message = "File uploaded successfully";
                }
            }
            else
            {
                userAccounts.ID = UserCurrent.ID;                
                userAccounts.IsActive = UserCurrent.IsActive;
                userAccounts.Amount = UserCurrent.Amount;
                userAccounts.IDRole = UserCurrent.IDRole;
                userAccounts.Age = UserCurrent.Age;                
                _userService.Update(userAccounts);
            }

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

        [HttpPost]
        public ActionResult ChangePassword(UserProfile userAccounts)
        {
            if (!userAccounts.Password.Equals(SHA1.Decrypt(UserCurrent.Password)))
            {
                ViewBag.ErrorOldPassword = "Password invalid! Please try again!";
                return View("Manage", UserCurrent);
            }
            else
            {
                if (!userAccounts.NewPassword.Equals(userAccounts.ConfirmPassword))
                {
                    ViewBag.ErrorConfirmPassword = "Password not match! Please try again! ";
                    return View("Manage", UserCurrent);
                }
                else
                {
                    userAccounts.ID = UserCurrent.ID;
                    userAccounts.Password = SHA1.Encode(userAccounts.NewPassword);
                    if (_userService.ChangePassword(userAccounts))
                    {
                        return RedirectToAction("LogOut", "Account");
                    }
                    else
                    {
                        return View("Manage", UserCurrent);
                    }
                }
            }
        }
        #endregion

    }
}
