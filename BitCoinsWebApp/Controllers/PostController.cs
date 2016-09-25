namespace BitCoinsWebApp.Controllers
{
    using BitCoinsWebApp.Model;
    using BitCoinsWebApp.Utilities;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    [SessionExpire]
    public class PostController : BaseController
    {      

        public ActionResult Index()
        {
            return View(GetPost);
        }
        
        public ActionResult AddPost() 
        {
            return View(SetPost);
        }

        [HttpPost]
        public ActionResult NewPost(PostNews post,HttpPostedFileBase uploadFile) 
        {
            if (ModelState.IsValid)
            {
                if (uploadFile == null)
                {
                    ViewBag.Message = "Please Upload Your file"; return View("AddPost", SetPost);
                }
                else if (uploadFile.ContentLength > 0)
                {
                    int MaxContentLength = 1024 * 1024 * 3; //3 MB
                    string[] AllowedFileExtensions = new string[] { ".jpg", ".gif", ".png", ".pdf" };

                    if (!AllowedFileExtensions.Contains(uploadFile.FileName.Substring(uploadFile.FileName.LastIndexOf('.'))))
                    {
                        ViewBag.Message = "Please file of type: " + string.Join(", ", AllowedFileExtensions);
                        return View("AddPost", SetPost);
                    }

                    else if (uploadFile.ContentLength > MaxContentLength)
                    {
                        ViewBag.Message= "Your file is too large, maximum allowed size is: " + MaxContentLength + " MB";
                        return View("AddPost", SetPost);
                    }
                    else
                    {
                        //TO:DO
                        var fileName = Path.GetFileName(uploadFile.FileName);
                        var path = Path.Combine(Server.MapPath("~/Content/Images"), fileName);
                        uploadFile.SaveAs(path);
                        post.UserPost = UserCurrent;
                        _postService.CreatePost(post, "~/Content/Images/" + fileName);
                        ModelState.Clear();
                        ViewBag.Message = "File uploaded successfully";
                    }
                }
            }            
            return View("Index",GetPost);
        }       

    }
}
