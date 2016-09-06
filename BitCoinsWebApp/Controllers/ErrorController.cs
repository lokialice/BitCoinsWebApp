﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BitCoinsWebApp.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Error404()
        {
            return View("404");
        }

        public ActionResult Error400()
        {
            return View("400");
        }

        public ActionResult Error403()
        {
            return View("403");
        }

        public ActionResult Error500()
        {
            return View("500");
        }

        public ActionResult Error503()
        {
            return View("503");
        }
    }
}
