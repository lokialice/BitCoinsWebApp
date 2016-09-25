using BitCoinsWebApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BitCoinsWebApp.BLL
{
    public interface IPostService
    {
        List<PostNews> GetAllListPost();
        bool CreatePost(PostNews post, string filePath);
    }
}
