using System.Web.Mvc;
using System.Web.Routing;
using CC.Service.Webhost.CodeCampSvc;
using Ninject;

namespace CC.UI.Webhost
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            int id;
            var service = new CodeCampService();

            var defaultEvent = service.GetDefaultEvent();
            id = defaultEvent.ID;

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { eventid = id, controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "Archive", // Route name
                "Archive/{eventid}/{controller}/{action}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }
    }
}