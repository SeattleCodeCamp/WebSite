using System.Linq;

namespace OCC.UI.Webhost.Controllers
{
    using System;
    using System.Web.Mvc;
    using UiModel = OCC.UI.Webhost.Models;
    using OCC.UI.Webhost.CodeCampService;
    using System.Web;
    using System.Web.Helpers;
    using System.IO;
    using LinqToTwitter;
    
    public abstract class BaseController : Controller
    {
        protected ICodeCampService service;

        public BaseController(ICodeCampService service)
        {
            this.service = service;
        }

        public BaseController()
            : this(new CodeCampServiceClient())
        {
        }

        public UiModel.Person CurrentUser
        {
            get 
            {
               return HttpContext.Session["person"] as UiModel.Person;
            }
            set
            {
                HttpContext.Session["person"] = value;
            }
        }

        protected string GetImageInfo(string twitterName, string defaultURL)
        {
            if (twitterName == null)
                return defaultURL;
            string twitterURL = defaultURL;
            var auth = new MvcAuthorizer();
            SessionStateCredentials s = new SessionStateCredentials();
            s.ConsumerKey = "fUpi8KuU3hMWsCHueZIww";
            s.ConsumerSecret = "4gCFXwi5zW5CYIoGSNgydL9dmqVM9T9BUS9ElrMI";
            auth.Credentials = s;
            var twitterCtx = new TwitterContext(auth);
            try
            {
                var userResponse =
                     (from user in twitterCtx.User
                      where user.Type == UserType.Lookup &&
                            user.ScreenName == twitterName.Replace("@", "")
                      select user).ToList();

                if (userResponse == null)
                {
                    return defaultURL;
                }
                twitterURL = userResponse.FirstOrDefault().ProfileImageUrl;
            }
            catch
            {
                return defaultURL;
            }
            return twitterURL;
        }

        //"../../Content/avatar"
        protected string GetImageInfo(HttpPostedFileBase file, string destinationFolder)
        {
            string imgUrl = string.Empty;
            var postedFile = file;
            if (postedFile.ContentLength > 0)
            {
                try
                {
                    WebImage rewrittenImage = RewritePostedImage(file);
                    var persistedPath = string.Format("../..{0}/{1}", destinationFolder, rewrittenImage.FileName);
                    string fullPath = Path.Combine(Server.MapPath(destinationFolder), rewrittenImage.FileName);
                    postedFile.SaveAs(fullPath);
                    imgUrl = persistedPath;
                }
                catch (Exception e)
                {
                    string s = e.GetBaseException().Message;
                    // Swallow
                }

            }
            return imgUrl;
        }

        private WebImage RewritePostedImage(HttpPostedFileBase uploadedFile)
        {
            byte[] imageArray = new byte[uploadedFile.ContentLength];
            uploadedFile.InputStream.Read(imageArray, 0, uploadedFile.ContentLength);
                string[] fName = uploadedFile.FileName.Split('\\');
            var img = new WebImage(imageArray)
            {
                FileName = string.Format("{0}.{1}",
                                  DateTime.UtcNow.Ticks,
                                  fName[fName.Length-1])
            };
            return img;
        }
    }
}
