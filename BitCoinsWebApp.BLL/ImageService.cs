namespace BitCoinsWebApp.BLL
{
    using BitCoinsWebApp.DAL.Repositories;
    using BitCoinsWebApp.Model;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ImageService:IImageService
    {
        #region member
        private readonly IImageRepository _repository;
        private readonly string _connectionString;
        #endregion

        #region constructor
        public ImageService()
        {
             _connectionString = ConfigurationManager.ConnectionStrings["BitWebAppEntities"].ConnectionString;
            _repository = new ImageRepository(_connectionString);
        }
        #endregion
        public bool Create(ImageFileUpload img)
        {
            return _repository.Create(img);
        }

        public ImageFileUpload GetLasttestImg()
        {
            return _repository.GetLasttestImg();
        }
    }
}
