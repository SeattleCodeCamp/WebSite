using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using OCC.UI.Webhost.App_Start;

namespace CC.UI.Webhost
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            //if (!HttpContext.Current.IsDebuggingEnabled)
            //{
            //    filters.Add(new RequireHttpsAttribute());
            //}

            filters.Add(new RequireHttpsAttribute());
        }

        //public static void RegisterRoutes(RouteCollection routes)
        //{
        //    routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

        //    // 1. dotnetda.org -> goes to the current event
        //    // 2. dotnetda.org/sessions
        //    // 2. dotnetda.org/occ2012
        //    // 3. dotnetda.org/occ2012/sessions

        //    int id;
        //    using (var service = new CodeCampServiceClient())
        //    {
        //        var defaultEvent = service.GetDefaultEvent();
        //        id = defaultEvent.ID;
        //    }

        //    routes.MapRoute(
        //        "Default", // Route name
        //        "{controller}/{action}/{id}", // URL with parameters
        //        new { eventid = id, controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
        //    );

        //    routes.MapRoute(
        //        "Archive", // Route name
        //        "Archive/{eventid}/{controller}/{action}", // URL with parameters
        //        new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
        //    );

        //    //routes.MapRoute(
        //    //    "Default", // Route name
        //    //    "{controller}/{action}/{id}", // URL with parameters
        //    //    new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
        //    //);
        //}

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            //HttpCookie authCookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];
            //if (authCookie == null || authCookie.Value == "")
            //    return;

            //FormsAuthenticationTicket authTicket;
            //try
            //{
            //    authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            //}
            //catch
            //{
            //    return;
            //}

            // Cast the Thread.CurrentPrincipal
            var identity = (ClaimsIdentity)User.Identity;

            // retrieve roles from UserData 
            var firstOrDefault = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (firstOrDefault != null)
            {
                string[] roles = firstOrDefault.Value.Split(';');

                if (Context.User != null)
                {
                    Context.User = new GenericPrincipal(Context.User.Identity, roles);
                }
            }
        }
    }
}