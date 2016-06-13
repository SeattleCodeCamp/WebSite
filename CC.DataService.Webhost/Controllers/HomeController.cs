using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OCC.DataService.Webhost.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Sessions()
        {
            var sessions = DataServiceEntities.Sessions.Expand("SessionAttendees").ToList();            
            return View(sessions);
            
        }
    }
}
