using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace certExam
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}",
                defaults: new { controller = "swagger/index.html?url=/swagger/v1/swagger.json" }
            );
            //routes.MapRoute(
            //    name: "Default",
            //    //url: "{controller}/{action}/{id}",
            //    url: "{controller}",
            //    //defaults: new { controller = "S00020", action = "S00020", id = UrlParameter.Optional },
            //    //defaults: new { controller = "ZhSystem", action = "ZhIndex", id = UrlParameter.Optional },
            //    //defaults: new { controller = "ZhSystem", action = "Login", id = UrlParameter.Optional },
            //    defaults: new { controller = "swagger" },
            //    //defaults: new { controller = "ZhFront", action = "Index", id = UrlParameter.Optional },
            //    namespaces: new[] { "Controllers" }
            //    //defaults: new { controller = "A0000Test", action = "Index2", id = UrlParameter.Optional }
            //);
        }
    }
}
