using BitCoinsWebApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitCoinsWebApp.BLL
{
    public interface IPostService
    {
        List<PostNews> GetAllListPost();
    }
}
