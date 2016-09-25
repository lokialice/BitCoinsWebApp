using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitCoinsWebApp.Model;
using System.Web;

namespace BitCoinsWebApp.DAL.Repositories
{
    public interface IPostRepository
    {
        List<PostNews> GetAllListPost();
        bool CreatePost(PostNews post, string filePath);        
    }
}
