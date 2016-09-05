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

namespace BitCoinsWebApp.DAL.Repositories
{
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

        public List<PostNews> GetAllListPost()
        {
            try
            {
                List<Post> listPost = new List<Post>();
                listPost = _pce.Posts.ToList();

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
    }
}
