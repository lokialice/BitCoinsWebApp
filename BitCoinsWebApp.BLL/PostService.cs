using BitCoinsWebApp.DAL.Repositories;
using BitCoinsWebApp.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitCoinsWebApp.BLL
{
    public class PostService :IPostService
    {
        #region member
        private readonly IPostRepository _repository;
        private readonly string _connectionString;
        #endregion

        #region constructor
        public PostService() 
        {
            _connectionString = ConfigurationManager.ConnectionStrings["BitWebAppEntities"].ConnectionString;
            _repository = new PostRepository(_connectionString);
        }
        #endregion

        public List<PostNews> GetAllListPost()
        {
            return _repository.GetAllListPost();
        }

        public bool CreatePost(PostNews post, string filePath)
        {
            return _repository.CreatePost(post, filePath);
        }

        public PostNews GetBlogDetail(int id)
        {
            return _repository.GetBlogDetail(id);
        }
    }
}
