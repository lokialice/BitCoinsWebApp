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
        protected readonly IFundService _fundService;
        protected readonly IPostService _postService;
        private UserProfile _userCurrent;
        private Transactions _userTransfer;
        private ManageModel _manageModel;
        private PostNews _getPost;
        
        #endregion

        #region constructor
        public BaseController() 
        {
            _userService = new UserService();
            _userTransfer = new Transactions();
            _fundService = new FundService();
            _manageModel = new ManageModel();
            _postService = new PostService();
            _getPost = new PostNews();
           
        }
        #endregion

        #region public member

        public UserProfile UserCurrent
        {
            get 
            {
                _userCurrent = _userService.GetUserByUserName(Session["UserLogin"].ToString());
                _userCurrent.ListRef = _userService.GetRefByUsername(_userCurrent.UserName);
                return _userCurrent;
            }
           
        }

        public Transactions UserTransfer
        {
            get
            {               
                return _userTransfer; 
            }           
        }

        public IFundService FundService
        {
            get { return _fundService; }
        }

        public ManageModel ManageModel
        {
            get 
            {
                _manageModel.UserManage = UserCurrent;
                _manageModel.CountRefID = _userService.GetTotalRefID(UserCurrent);
                _manageModel.AccountBalance = _userService.GetAccountBalance(UserCurrent);
                _manageModel.ProInterestWallet = UserCurrent.Amount;
                return _manageModel; 
            }            
        }

        public PostNews GetPost
        {
            get 
            {
                _getPost.GetAllListPostNews = _postService.GetAllListPost();
                _getPost.UserPost = UserCurrent;
                return _getPost; 
            }           
        }    
        #endregion


    }
}
