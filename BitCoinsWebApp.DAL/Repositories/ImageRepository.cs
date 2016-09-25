using AutoMapper;
using BitCoinsWebApp.DAL.Entites;
using BitCoinsWebApp.Model;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitCoinsWebApp.DAL.Repositories
{
    public class ImageRepository:IImageRepository
    {
        #region member
        private readonly BitCoinsEntities _pce;
        private readonly string _connectionString;
        private static readonly ILog logger = LogManager.GetLogger(typeof(FundsRepository).Name);  //Declaring Log4Net    
        #endregion

        #region constructor
        public ImageRepository(string connectionString) 
        {
            _connectionString = connectionString;
            _pce = new BitCoinsEntities(connectionString);
        }
        #endregion
        public bool Create(ImageFileUpload img) 
        {
            try
            {
                Mapper.CreateMap<ImageFileUpload, ImageUpload>();
                ImageUpload mappedImage = Mapper.Map<ImageFileUpload, ImageUpload>(img);
                mappedImage.CreateDate = DateTime.Now;
                _pce.AddToImageUploads(mappedImage);
                _pce.SaveChanges();
                logger.Info("Complete Create(ImageUploads img)");               
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return false;
            }
        }


        public ImageFileUpload GetLasttestImg()
        {
            try
            {
                Mapper.CreateMap<ImageUpload, ImageFileUpload>();
                ImageUpload img = _pce.ImageUploads.OrderByDescending(p => p.CreateDate).FirstOrDefault();
                ImageFileUpload mappedImage = Mapper.Map<ImageUpload, ImageFileUpload>(img);
                logger.Info("Complete GetLasttestImg");
                return mappedImage;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return null;
            }
        }
    }
}
