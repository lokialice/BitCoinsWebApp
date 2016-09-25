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
        private PostNews _getPost;
        #endregion

        #region public member
        //public PostNews GetPost
        //{
        //    get 
        //    {
        //        _getPost.GetAllListPostNews = _postService.GetAllListPost().Take(2).ToList();
        //        return _getPost; 
        //    }           
        //}
        #endregion

        public HomeController() 
        {
            _postService = new PostService();
            _getPost = new PostNews();
        }

        public ActionResult Index()
        {          
           
            return View();
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

    }
}
