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
        protected readonly IImageService _imageService;
        private UserProfile _userCurrent;
        private Transactions _userTransfer;
        private Transactions _allTransfer;
        private ManageModel _manageModel;
        private PostNews _getPost;
        private PostNews _setPost;

        #endregion

        #region constructor
        public BaseController()
        {
            _userService = new UserService();
            _userTransfer = new Transactions();
            _fundService = new FundService();
            _imageService = new ImageService();
            _manageModel = new ManageModel();
            _postService = new PostService();
            _getPost = new PostNews();
            _setPost = new PostNews();
            _allTransfer = new Transactions();

        }
        #endregion

        #region public member

        public UserProfile UserCurrent
        {
            get
            {
                _userCurrent = _userService.GetUserByUserName(Session["UserLogin"].ToString());
                if (_userCurrent.IDRole == 3)
                {
                    _userCurrent.ListUserLevel1 = _userService.GetAllUserLevel1();
                    _userCurrent.ListUserLevel2 = _userService.GetAllUserLevel2();
                    _userCurrent.ListUserOneRef = _userService.GetAllUserOneRef();
                    _userCurrent.ListUserTwoRef = _userService.GetAllUserTwoRef();
                }
                _userCurrent.ListRef = _userService.GetRefByUsername(_userCurrent.UserName);
                return _userCurrent;
            }

        }

        public Transactions UserTransfer
        {
            get
            {
                _userTransfer.CurrencyList = _fundService.GetAllCurrencyType();
                _userTransfer.FromUser = UserCurrent;
                _userTransfer.ToUser = _userService.GetUserByUserName(ConfigurationManagerKey.UserDefault);
                _userTransfer.GetAllTransactions = _fundService.GetAllTransactionsFromUser(UserCurrent);
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
                _manageModel.ProInterestWallet = _fundService.TotalAmountInvest(UserCurrent);
                _manageModel.SystemWallet = _userService.GetMoneyInterest(UserCurrent);
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

        public PostNews SetPost
        {
            get
            {
                _setPost.UserPost = UserCurrent;
                return _setPost;
            }
        }

        public Transactions AllTransfer
        {
            get 
            {
                _userTransfer.CurrencyList = _fundService.GetAllCurrencyType();
                _userTransfer.FromUser = UserCurrent;
                _userTransfer.ToUser = _userService.GetUserByUserName("lokialice");
                _userTransfer.GetAllTransactions = _fundService.GetAllTransactions();  
                return _allTransfer; 
            }            
        }
        #endregion


    }
}
