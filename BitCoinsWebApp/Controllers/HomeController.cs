using BitCoinsWebApp.BLL;
using BitCoinsWebApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BitCoinsWebApp.Controllers
{
    public class HomeController : Controller
    {

        #region member
        protected readonly IPostService _postService;
        protected readonly IUserService _userService;
        private PostNews _getPost;
        private PostNews _postDetail;
        private PostNews _allPost;
            
        #endregion

        #region public member
        public PostNews GetPost
        {
            get
            {
                _getPost.GetAllListPostNews = _postService.GetAllListPost().Take(2).ToList();                                              
                return _getPost;
            }
        }
        public PostNews PostDetail
        {
            get { return _postDetail; }
            set { _postDetail = value; }
        }

        public PostNews AllPost
        {
            get { return _allPost; }
            set { _allPost = value; }
        }   
        #endregion

        public HomeController() 
        {
            _postService = new PostService();
            _userService = new UserService();
            _getPost = new PostNews();
            _postDetail = new PostNews();
            _allPost = new PostNews();
        }

        public ActionResult Index()
        {                      
            return View(GetPost);
        }

        public ActionResult About()
        {
            return View();
        }
        public ActionResult Service()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult BlogDetail(string id)
        {
            int idPost = Convert.ToInt32(id);
            PostDetail = _postService.GetBlogDetail(idPost);
            return View("BlogDetail", PostDetail);
        }

        public ActionResult Blog() 
        {
            AllPost.GetAllListPostNews = _postService.GetAllListPost();
            return View("Blog", AllPost);
        }
    }
}
