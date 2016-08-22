using BitCoinsWebApp.BLL;
using BitCoinsWebApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BitCoinsWebApp.Controllers
{
    public class BaseController : Controller
    {
        #region member
        protected readonly IUserService _userService;
        private UserProfile _userCurrent;
       
        #endregion

        #region constructor
        public BaseController() 
        {
            _userService = new UserService();
            
           
        }
        #endregion

        #region public member
        public UserProfile UserCurrent
        {
            get { return _userCurrent = _userService.GetUserByUserName(Session["UserLogin"].ToString()); ; }

            set { _userCurrent = _userService.GetUserByUserName(Session["UserLogin"].ToString()); }
        }
        #endregion


    }
}
