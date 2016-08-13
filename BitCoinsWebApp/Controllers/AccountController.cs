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
        
        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]       
        public ActionResult Login(User userAccount)
        {            
            User user = _userService.Login(userAccount.UserName, userAccount.Password,true);
            if (user != null)
            {
                Session["UserLogin"] = user.UserName;
                return View("Index");
            }
            else
            {
                ViewBag.Error = "Fuck you";
                return View("Index");
            }
        }
      
    }
}
