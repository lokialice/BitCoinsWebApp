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
    public class PostController : BaseController
    {      

        public ActionResult Index()
        {
            return View(GetPost);
        }
        
        public ActionResult AddPost() 
        {
            return View(SetPost);
        }

        [HttpPost]        
        public ActionResult AddPost(PostNews post) 
        {
            return View("Index",GetPost);
        }

    }
}
