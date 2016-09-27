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
    using System.IO;

    public class PostRepository : IPostRepository
    {
        #region member
        private readonly BitCoinsEntities _pce;
        private IImageRepository _imageRepository;
        private IUserRepository _userRepository;
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
                _userRepository = new UserRepository(_connectionString);
                List<Post> listPost = new List<Post>();
                listPost = _pce.Posts.OrderByDescending(p => p.PostDate).ToList();

                if (listPost == null && listPost.Count() == 0)
                {
                    return null;
                }

                List<PostDTO> listPostDTO = new List<PostDTO>();
                PostDTO post;
                ImageFileUpload img;
                foreach (var item in listPost)
                {
                    post = new PostDTO();
                    img = new ImageFileUpload();
                    post.ID = item.ID;
                    post.PostAuthor = item.PostAuthor;
                    post.PostContent = item.PostContent;
                    post.PostDate = item.PostDate;
                    post.PostExcerpt = item.PostExcerpt;
                    post.PostStatus = (bool)item.PostStatus;
                    post.PostTittle = item.PostTittle;
                    img.ID = item.ImageUpload.ID;
                    img.ImageFile = item.ImageUpload.ImageFile;
                    img.ImageName = item.ImageUpload.ImageName;
                    post.FeatureImageURL = img;
                    post.UserPost = _userRepository.GetUser(item.PostAuthor);
                    listPostDTO.Add(post);
                }
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
                _imageRepository = new ImageRepository(_connectionString);
                ImageFileUpload img = new ImageFileUpload();
                img.CreateDate = DateTime.Now;
                img.ImageFile = filePath;
                img.ImageName = Path.GetFileName(filePath);
                _imageRepository.Create(img);
                post.FeatureImageURL = _imageRepository.GetLasttestImg();
                mappedPost.FeatureImage = post.FeatureImageURL.ID;
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

        public PostDTO GetBlogDetail(int id)
        {
            try
            {
                _userRepository = new UserRepository(_connectionString);
                Post post = _pce.Posts.FirstOrDefault(m => m.ID == id);
                PostDTO mappedPost = new PostDTO();
                ImageFileUpload img = new ImageFileUpload();
                if (post == null)
                {
                    return null;
                }
                mappedPost.ID = post.ID;
                mappedPost.PostAuthor = post.PostAuthor;
                mappedPost.PostContent = post.PostContent;
                mappedPost.PostDate = post.PostDate;
                mappedPost.PostExcerpt = post.PostExcerpt;
                mappedPost.PostStatus = (bool)post.PostStatus;
                mappedPost.PostTittle = post.PostTittle;
                img.ID = post.ImageUpload.ID;
                img.ImageFile = post.ImageUpload.ImageFile;
                img.ImageName = post.ImageUpload.ImageName;
                mappedPost.FeatureImageURL = img;
                mappedPost.UserPost = _userRepository.GetUser(post.PostAuthor);
                return mappedPost;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return null;
            }
        }
        #endregion

    }
}
