using BitCoinsWebApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitCoinsWebApp.DAL.Repositories
{
    public interface IImageRepository
    {
        bool Create(ImageFileUpload img);
        ImageFileUpload GetLasttestImg();
    }
}
