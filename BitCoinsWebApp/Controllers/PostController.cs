namespace BitCoinsWebApp.Controllers
{
    using BitCoinsWebApp.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    [SessionExpire]
    public class PostController : BaseController
    {      

        public ActionResult Index()
        {
            return View(GetPost);
        }

        public ActionResult AddPost() 
        {
            return View();
        }


    }
}
