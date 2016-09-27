using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BitCoinsWebApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "Register",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Account", action = "Register", id = UrlParameter.Optional }
           );
            routes.MapRoute(
              name: "Balance",
              url: "{controller}/{action}/{id}",
              defaults: new { controller = "Account", action = "Balance", id = UrlParameter.Optional }
          );
            routes.MapRoute(
             name: "ActiveUser",
             url: "{controller}/{action}/{id}",
             defaults: new { controller = "Manage", action = "ActiveUser", id = UrlParameter.Optional }
         );
            routes.MapRoute(
             name: "ActiveOrder",
             url: "{controller}/{action}/{id}/{type}",
             defaults: new { controller = "Fund", action = "ActiveOrder", id = UrlParameter.Optional }
         );
            routes.MapRoute(
             name: "DefaultUser",
             url: "{controller}/{action}/{id}",
             defaults: new { controller = "Manage", action = "DefaultUser", id = UrlParameter.Optional }
         );
            routes.MapRoute(
            name: "GetBlogDetail",
            url: "{controller}/{action}/{id}",
            defaults: new { controller = "Post", action = "GetBlogDetail", id = UrlParameter.Optional }
        );
        }
    }
}