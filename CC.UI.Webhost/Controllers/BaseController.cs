using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using LinqToTwitter;
using CC.Service.Webhost.CodeCampSvc;
using CC.UI.Webhost.Models;
using UiModel = CC.UI.Webhost.Models;

namespace CC.UI.Webhost.Controllers
{
    public abstract class BaseController : Controller
    {
        protected ICodeCampService service;
        protected ICodeCampServiceRepository repo;

        public BaseController(ICodeCampService service, ICodeCampServiceRepository repo)
        {
            this.service = service;
            this.repo = repo;
        }

        public BaseController()
            : this(new CodeCampService(), new CodeCampServiceRepository(new CodeCampService()))
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

            var twitterUrl = defaultURL;

            var auth = new MvcAuthorizer
            {
                CredentialStore = new SessionStateCredentialStore
                {
                    ConsumerKey = "fUpi8KuU3hMWsCHueZIww",
                    ConsumerSecret = "4gCFXwi5zW5CYIoGSNgydL9dmqVM9T9BUS9ElrMI"
                }
            };


            var twitterCtx = new TwitterContext(auth) {ReadWriteTimeout = 300};
            try
            {
                var userResponse =
                     (from user in twitterCtx.User
                      where user.Type == UserType.Show &&
                            user.ScreenName == twitterName.Replace("@", "")
                      select user).ToArray();

                var firstOrDefault = userResponse.FirstOrDefault();
                if (firstOrDefault != null) twitterUrl = firstOrDefault.ProfileImageUrl;
            }
            catch(Exception)
            {
                return defaultURL;
            }
            return twitterUrl;
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
                                  fName[fName.Length - 1])
            };
            return img;
        }
    }
}
