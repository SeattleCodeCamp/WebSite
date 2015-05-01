using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OCC.DataService.Webhost.Controllers
{
    public class BaseController : Controller
    {
        const string url = "DataService.svc";

        ODataService.OrlandoCodeCampEntities dataServiceEntities;
        protected ODataService.OrlandoCodeCampEntities DataServiceEntities
        {
            get
            {
                return dataServiceEntities ?? (dataServiceEntities = new ODataService.OrlandoCodeCampEntities(Uri));
            }
        }

        Uri Uri
        {
            get
            {
                var strUrl = string.Format("http://{0}:{1}/DataService.svc",Request.Url.Host,Request.Url.Port);
                return new Uri(strUrl);
            }
        }

    }
}
