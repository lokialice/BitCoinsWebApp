using BitCoinsWebApp.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BitCoinsWebApp.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IUserService _userService;

        public BaseController() 
        {
            _userService = new UserService();
        }

    }
}
