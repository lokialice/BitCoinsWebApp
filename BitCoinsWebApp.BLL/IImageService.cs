using BitCoinsWebApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitCoinsWebApp.BLL
{
    public interface IImageService
    {
        bool Create(ImageFileUpload img);
        ImageFileUpload GetLasttestImg();
    }
}
