namespace BitCoinsWebApp.DAL.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BitCoinsWebApp.Model;
    using PostDTO = BitCoinsWebApp.Model.PostNews;
    using BitCoinsWebApp.DAL.Entites;
    using log4net;
    using AutoMapper;
    using System.Web;

    public class PostRepository : IPostRepository
    {
        #region member
        private readonly BitCoinsEntities _pce;
        private readonly string _connectionString;
        private static readonly ILog logger = LogManager.GetLogger(typeof(FundsRepository).Name);  //Declaring Log4Net  
        #endregion

        #region constructor
        public PostRepository(string connectionString)
        {
            _connectionString = connectionString;
            _pce = new BitCoinsEntities(connectionString);
        }
        #endregion

        #region method
        public List<PostNews> GetAllListPost()
        {
            try
            {
                List<Post> listPost = new List<Post>();
                listPost = _pce.Posts.OrderByDescending(p=> p.PostDate).ToList();

                if (listPost == null && listPost.Count() == 0)
                {
                    return null;
                }
                Mapper.CreateMap<Post, PostDTO>();
                List<PostDTO> listPostDTO = Mapper.Map<List<Post>, List<PostDTO>>(listPost);
                logger.Info("Complete GetAllListPost");
                return listPostDTO;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return null;
            }
        }

        public bool CreatePost(PostNews post, string filePath)
        {
            try
            {
                Mapper.CreateMap<PostDTO, Post>();
                Post mappedPost = Mapper.Map<PostDTO, Post>(post);
                mappedPost.FeatureImage = filePath;
                mappedPost.PostDate = DateTime.Now;
                mappedPost.PostStatus = true;
                mappedPost.PostAuthor = post.UserPost.ID;
                _pce.AddToPosts(mappedPost);
                _pce.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return false;
            }
        }
        #endregion
    }
}
