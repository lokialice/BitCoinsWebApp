namespace BitCoinsWebApp.Controllers
{
    using BitCoinsWebApp.Model;
    using BitCoinsWebApp.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    [SessionExpire]
    public class ManageController : BaseController
    {
        #region member
        
        #endregion

        #region public member       
        #endregion

        #region method
        [HttpGet]
        public ActionResult Index()
        {           
            return View(ManageModel);
        }

        public ActionResult UserLevel1() 
        {
            return View("UserLevel1", UserCurrent);
        }

        public ActionResult UserLevel2()
        {
            return View("UserLevel2", UserCurrent);
        }
        
        [HttpPost]
        public ActionResult ActiveUser(string id) 
        {
            _userService.ActiveUser(id);
            return View("UserLevel1",UserCurrent);
        }

        [HttpPost]
        public ActionResult DefaultUser(string id)
        {
            _userService.DefaultUser(id);
            return View("Manage", UserCurrent);
        }


        #endregion


    }
}
