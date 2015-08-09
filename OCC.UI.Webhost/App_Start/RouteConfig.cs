using System.Web.Mvc;
using System.Web.Routing;
using OCC.UI.Webhost.CodeCampService;

namespace OCC.UI.Webhost
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // 1. dotnetda.org -> goes to the current event
            // 2. dotnetda.org/sessions
            // 2. dotnetda.org/occ2012
            // 3. dotnetda.org/occ2012/sessions

            int id;
            using (var service = new CodeCampServiceClient())
            {
                var defaultEvent = service.GetDefaultEvent();
                id = defaultEvent.ID;
            }

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