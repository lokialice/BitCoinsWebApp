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
            return View(UserCurrent);
        }
        #endregion


    }
}
