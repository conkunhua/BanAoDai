using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BanAoDai
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
             name: "Home",
             url: "trang-chu",
             defaults: new { controller = "Site", action = "Index", id = UrlParameter.Optional }
         );
            routes.MapRoute(
             name: "Contacts",
             url: "lien-he",
             defaults: new { controller = "Contacts", action = "Index", id = UrlParameter.Optional }
         );

            routes.MapRoute(
              name: "Sect",
              url: "san-pham",
              defaults: new { controller = "Sect", action = "Index", id = UrlParameter.Optional }
          );

            routes.MapRoute(
                name: "SiteSlug",
                url: "{slug}",
                defaults: new { controller = "Site", action = "Index", id = UrlParameter.Optional }
            );
           

            routes.MapRoute(
               name: "Default",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Site", action = "Index", id = UrlParameter.Optional }
           );
        }
    }
}
