using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitCoinsWebApp.Model;

namespace BitCoinsWebApp.DAL.Repositories
{
    public interface IPostRepository
    {
        List<PostNews> GetAllListPost();
    }
}
