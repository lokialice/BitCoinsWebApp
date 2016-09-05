using BitCoinsWebApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BitCoinsWebApp.Controllers
{
    [SessionExpire]
    public class NetworkController : BaseController
    {
        //
        // GET: /Network/

        public ActionResult Index()
        {            
            return View("NetworkTree",UserCurrent);
        }

    }
}
