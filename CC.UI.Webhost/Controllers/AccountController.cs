using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CC.Service.Webhost.CodeCampSvc;
using CC.UI.Webhost.Infrastructure;
using CC.UI.Webhost.Models;
using HashProvider = CC.UI.Webhost.Infrastructure.UserNamePasswordHashProvider;
using uiModel = CC.UI.Webhost.Models;
using Services = CC.Service.Webhost.Services;
using CC.Service.Webhost.Tools;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace CC.UI.Webhost.Controllers
{


    public class AccountController : BaseController
    {

        private const string LocalImageUrl = @"/Content/Avatar/default_user_icon.jpg";
        private const string internalUserPwProvider = "internal";
        public AccountController(ICodeCampService service, ICodeCampServiceRepository repo) : base(service, repo)
        {
        }


        public PartialViewResult UserDisplayProfile()
        {
            var userDisplay = new UserDisplayProfileModel();

            if (CurrentUser != null)
            {
                if (CurrentUser.Identity.IsAuthenticated)
                {
                    userDisplay.DisplayFirstName = CurrentUser.FirstName;
                    userDisplay.DisplayLastName = CurrentUser.LastName;

                    if (string.IsNullOrEmpty(CurrentUser.ImageUrl))
                    {
                    }
                    else
                    {
                        try
                        {
                            //userDisplay.Avatar = new WebImageOCC(CurrentUser.ImageUrl);
                            //userDisplay.Avatar.Alt = CurrentUser.FirstName + " avatar image.";
                            //userDisplay.Avatar.Title = "User Profile Settings";
                            userDisplay.Avatar = CurrentUser.ImageUrl;
                        }
                        catch
                        {
                        }
                    }

                    userDisplay.IsLoggedIn = true;
                }
            }

            else
            {
                //HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                //if (authCookie != null)
                //{
                //    authCookie.Expires = DateTime.Now.AddDays(-1);
                //    Response.Cookies.Add(authCookie);
                //}
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie,
                              DefaultAuthenticationTypes.ExternalCookie);
                userDisplay.IsLoggedIn = false;
            }

            return PartialView("_UserDisplayProfile", userDisplay);
        }

        //
        //AccoUnt/LogOn
        public ActionResult LogOn()
        {
            return View();
        }

        //
        // POST: /Account/LogOn
        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl, FormCollection frm)
        {
            if (ModelState.IsValid)
            {
                Services.Person authenticatedPerson = null;
                Services.Person person = service.FindPersonByEmail(model.Email, internalUserPwProvider);
                if (person == null)
                {
                    ModelState.AddModelError("", "");
                    return View(model); //RedirectToAction("LogOn", "Account");
                }
                else
                {
                    person.PasswordHash = HashProvider.ComputePasswordHash(model.Password);
                    authenticatedPerson = service.Login(person);
                }

                if (authenticatedPerson != null)
                {
                    bool rememberMe = frm["rememberMe"] == "on";
                    SetFormsAuth(authenticatedPerson, rememberMe);
                    if (string.IsNullOrEmpty(authenticatedPerson.Location))
                    {
                        return RedirectToAction("UpdateProfile", "Account");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }
            return View(model);
        }

        //
        // GET: /Account/LogOff
        public ActionResult LogOff()
        {
            //FormsAuthentication.SignOut();

            HttpContext.Session.Abandon();

            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie,
                                DefaultAuthenticationTypes.ExternalCookie);

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register
        public ActionResult Register()
        {
            ViewBag.TShirtSizes = repo.GetTShirtSizes();

            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        public ActionResult Register(int eventId, RegisterModel model, FormCollection frm)
        {

            if (ModelState.IsValid)
            {
                ViewBag.TShirtSizes = repo.GetTShirtSizes();
                
                if (IsDuplicateRegistration(model.Email))
                {
                    ModelState.AddModelError("Email", "The user at this email address is already registered.");
                    return View(model);
                }

                if (!String.IsNullOrEmpty(model.Twitter))
                {
                    if (!model.Twitter.StartsWith("@"))
                    {
                        const string twitterPrefix = "@";
                        model.Twitter = string.Format("{0}{1}", twitterPrefix, model.Twitter);
                    }
                }

                var newPerson = new Services.Person()
                {
                    Email = model.Email,
                    PasswordHash = UserNamePasswordHashProvider.ComputePasswordHash(model.Password),
                    Twitter = model.Twitter,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Location = model.Location,
                    TShirtSize = model.TShirtSizeId,
                    LoginProvider = internalUserPwProvider
                };

                bool useTwitter = frm["cbTwitter"] == "on";
                if (model.Avatar != null)
                {
                    newPerson.ImageUrl = GetImageInfo(model.Avatar, "/Content/avatar");
                }
                else if (useTwitter)
                {
                    newPerson.ImageUrl = GetImageInfo(model.Twitter, LocalImageUrl);
                }
                else
                {
                    newPerson.ImageUrl = LocalImageUrl;
                }

                // service.RegisterPerson(newPerson);

                newPerson.ID = service.RegisterPerson(newPerson); // service.FindPersonByEmail(newPerson.Email).ID;

                service.Rsvp(eventId, newPerson.ID, "YES");

                this.CurrentUser = new uiModel.Person
                {
                    ID = newPerson.ID,
                    ImageUrl = newPerson.ImageUrl,
                    Website = newPerson.Website,
                    Email = newPerson.Email,
                    Bio = newPerson.Bio,
                    Twitter = newPerson.Twitter,
                    Blog = newPerson.Blog,
                    Title = newPerson.Title,
                    FirstName = newPerson.FirstName,
                    LastName = newPerson.LastName,
                    IsAdmin = newPerson.IsAdmin,
                    Location = newPerson.Location
                };
                HttpContext.User = this.CurrentUser;

                HttpContext.Session.Add("auth", true);
                //this.CurrentUser = newPerson.Map();
                //FormsAuthentication.SetAuthCookie(CurrentUser.Email, false); // ???

                CreateAuthTicket(false);

                return RedirectToAction("Index", "Home");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePassword
        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword
        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid && this.CurrentUser.Identity.IsAuthenticated)
            {
                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    var newPasswordHash = UserNamePasswordHashProvider.ComputePasswordHash(model.NewPassword);
                    var oldPasswordHash = UserNamePasswordHashProvider.ComputePasswordHash(model.OldPassword);

                    service.ChangePassword(this.CurrentUser.ID, oldPasswordHash, newPasswordHash);
                    changePasswordSucceeded = true;
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess
        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        // GET: /Account/UpdateProfile
        public ActionResult UpdateProfile()
        {
            if (this.CurrentUser == null)
            {
                return RedirectToAction("LogOn", "Account");
            }

            if (string.IsNullOrEmpty(this.CurrentUser.Location))
            {
                ViewBag.Message = "Please update your location";
            }

            ViewBag.TShirtSizes = repo.GetTShirtSizes();

            return View(this.CurrentUser);
        }

        [HttpPost]
        public ActionResult UpdateProfile(uiModel.Person person, FormCollection frm)
        {
            bool useTwitter = frm["cbTwitter"] == "on";
            if (person.Avatar != null)
            {
                person.ImageUrl = GetImageInfo(person.Avatar, "/Content/avatar");
            }
            else if (useTwitter)
            {
                person.ImageUrl = GetImageInfo(person.Twitter, LocalImageUrl);
            }
            else
            {
                person.ImageUrl = LocalImageUrl;
            }

            //string txtAvatarURL = frm["txtAvatarURL"];
            //bool useTwitter = frm["cbTwitter"] == "on";
            //if (useTwitter)
            //    person.ImageUrl = GetImageInfo(person.Twitter, localImageUrl);
            //else
            //    person.ImageUrl = string.IsNullOrEmpty(txtAvatarURL) ? localImageUrl : txtAvatarURL;


            //TODO - Can't update avatar after initial save due to filesystem security
            //if (String.IsNullOrEmpty(person.ImageUrl))
            //{
            //    person.ImageUrl = this.GetImageInfo(person.Avatar);
            //}
            //else
            //{
            //    string tempPersonUri = person.ImageUrl;
            //    person.ImageUrl =
            //        string.Format("{0}.{1}",
            //                      DateTime.UtcNow.Ticks.ToString(CultureInfo.InvariantCulture),
            //                      tempPersonUri);
            //    person.Avatar.FileName = person.ImageUrl;
            //}
            service.UpdatePerson(person.Transform());

            this.CurrentUser = person;

            return RedirectToAction("Index", "Home");
        }

        // GET: /Account/ResetPassword
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var attendee = service.FindPersonByEmail(model.Email, internalUserPwProvider);

                if (attendee == null)
                {
                    ModelState.AddModelError("", "We could not locate your account. Please register.");
                    return View();
                }


                bool resetPasswordSucceeded = false;
                try
                {
                    /*string temporaryPassword = RandomPasswordGenerator.Generate();*/
                    var temporaryPassword = System.Web.Security.Membership.GeneratePassword(11, 1);
                    var temporaryPasswordHash = UserNamePasswordHashProvider.ComputePasswordHash(temporaryPassword);

                    service.ResetPassword(model.Email, temporaryPassword, temporaryPasswordHash);
                    resetPasswordSucceeded = true;
                }
                catch (Exception)
                {
                    resetPasswordSucceeded = false;
                }

                if (resetPasswordSucceeded)
                {
                    return RedirectToAction("LogOn", "Account");
                }
                else
                {
                    ModelState.AddModelError("", "The was an error resetting your password, please try again later.");
                }
            }

            return View();
        }

        private void SetFormsAuth(Services.Person authenticatedPerson, bool rememberMe)
        {
            this.CurrentUser = new uiModel.Person
            {
                ID = authenticatedPerson.ID,
                ImageUrl = authenticatedPerson.ImageUrl,
                Website = authenticatedPerson.Website,
                Email = authenticatedPerson.Email,
                Bio = authenticatedPerson.Bio,
                Twitter = authenticatedPerson.Twitter,
                Blog = authenticatedPerson.Blog,
                Title = authenticatedPerson.Title,
                FirstName = authenticatedPerson.FirstName,
                LastName = authenticatedPerson.LastName,
                IsAdmin = authenticatedPerson.IsAdmin,
                Location = authenticatedPerson.Location,
                LoginProvider = authenticatedPerson.LoginProvider
            };

            HttpContext.Session.Add("auth", true);
            HttpContext.User = CurrentUser;

            SetPersonRoles(this.CurrentUser);

            CreateAuthTicket(rememberMe);

            // FormsAuthentication.SetAuthCookie(CurrentUser.Email, false);
            // ???
        }

        private void SetPersonRoles(Models.Person authenticatedPerson)
        {
            var defaultEvent = service.GetDefaultEvent();
            var speaker = service.GetSpeaker(defaultEvent.ID, authenticatedPerson.ID);
            if (speaker != null)
                authenticatedPerson.SetRolesForPerson(speaker);
        }

        private bool IsDuplicateRegistration(string email)
        {
            var p = this.service.FindPersonByEmail(email) as Services.Person;

            return (p != null);
        }

        private void CreateAuthTicket(bool rememberMe)
        {
            int roleCount = this.CurrentUser.Roles.Count;
            var sb = new StringBuilder();

            for (int i = 0; i < roleCount; i++)
            {
                sb.AppendFormat("{0}", this.CurrentUser.Roles[i]);
                if (i != roleCount - 1)
                {
                    sb.Append(";");
                }
            }

            var claims = new List<Claim>();

            // create required claims
            claims.Add(new Claim(ClaimTypes.NameIdentifier, this.CurrentUser.ID.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, this.CurrentUser.Email));

            // custom - roles
            claims.Add(new Claim(ClaimTypes.Role, sb.ToString()));

            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

            AuthenticationManager.SignIn(new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = rememberMe,
                ExpiresUtc = DateTime.UtcNow.AddDays(7)
            }, identity);

            //var authTicket = new FormsAuthenticationTicket(
            //1,                            // version                             
            //this.CurrentUser.Email,       // name        
            //DateTime.Now,                 // issueDate
            //DateTime.Now.AddDays(7),  // expirationDate
            //rememberMe,                        // persistent across sessions        
            //sb.ToString());               // string-based state bag I used for our roles.

            //string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            //var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            //HttpContext.Response.AppendCookie(authCookie);
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }



        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            ExternalLoginInfo loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("LogOn");
            }

            Services.Person authenticatedPerson = null;
            Services.Person person = service.FindPersonByEmail(loginInfo.Email);
            if (person == null)
            {
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                ViewBag.TShirtSizes = repo.GetTShirtSizes();
                return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
            else
            {
                if (person.LoginProvider != loginInfo.Login.LoginProvider)
                {
                    person.LoginProvider = loginInfo.Login.LoginProvider;
                    //service.ChangePassword(person.ID, person.PasswordHash, null);
                    service.UpdatePerson(person);
                }
                authenticatedPerson = service.Login(person);
            }

            if (authenticatedPerson != null)
            {
                //bool rememberMe = frm["rememberMe"] == "on";
                SetFormsAuth(authenticatedPerson, false);
                if (string.IsNullOrEmpty(authenticatedPerson.Location))
                {
                    return RedirectToAction("UpdateProfile", "Account");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
            }
            return RedirectToAction("LogOn");

        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(int eventId, ExternalLoginConfirmationViewModel model, string returnUrl, FormCollection frm)
        //public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }


                ViewBag.TShirtSizes = repo.GetTShirtSizes();
                Services.Person newPerson = new Services.Person();

                if (IsDuplicateRegistration(model.Email))
                {
                    ModelState.AddModelError("Email", "The user at this email address is already registered.");
                    return View(model);
                }
                else
                {
                    newPerson = new Services.Person()
                    {
                        Email = model.Email,
                        //PasswordHash = UserNamePasswordHashProvider.ComputePasswordHash(model.Password),
                        Twitter = model.Twitter,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Location = model.Location,
                        TShirtSize = model.TShirtSizeId,
                        LoginProvider = model.LoginProvider
                    };
                }
                
                if (!String.IsNullOrEmpty(model.Twitter))
                {
                    if (!model.Twitter.StartsWith("@"))
                    {
                        const string twitterPrefix = "@";
                        model.Twitter = string.Format("{0}{1}", twitterPrefix, model.Twitter);
                    }
                }

                bool useTwitter = frm["cbTwitter"] == "on";
                //bool useTwitter = false;
                if (model.Avatar != null)
                {
                    newPerson.ImageUrl = GetImageInfo(model.Avatar, "/Content/avatar");
                }
                else if (useTwitter)
                {
                    newPerson.ImageUrl = GetImageInfo(model.Twitter, LocalImageUrl);
                }
                else
                {
                    newPerson.ImageUrl = LocalImageUrl;
                }

                // service.RegisterPerson(newPerson);

                newPerson.ID = service.RegisterPerson(newPerson); // service.FindPersonByEmail(newPerson.Email).ID;


                //service.GetDefaultEvent().ID
                service.Rsvp(eventId, newPerson.ID, "YES");
                //service.Rsvp(service.GetDefaultEvent().ID, newPerson.ID, "YES");

                this.CurrentUser = new uiModel.Person
                {
                    ID = newPerson.ID,
                    ImageUrl = newPerson.ImageUrl,
                    Website = newPerson.Website,
                    Email = newPerson.Email,
                    Bio = newPerson.Bio,
                    Twitter = newPerson.Twitter,
                    Blog = newPerson.Blog,
                    Title = newPerson.Title,
                    FirstName = newPerson.FirstName,
                    LastName = newPerson.LastName,
                    IsAdmin = newPerson.IsAdmin,
                    Location = newPerson.Location,
                    LoginProvider = newPerson.LoginProvider
                };

                HttpContext.User = this.CurrentUser;

                HttpContext.Session.Add("auth", true);
                //this.CurrentUser = newPerson.Map();
                //FormsAuthentication.SetAuthCookie(CurrentUser.Email, false); // ???

                CreateAuthTicket(false);

                return RedirectToLocal(returnUrl);

                //var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                //var result = await UserManager.CreateAsync(user);
                //if (result.Succeeded)
                //{
                //    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                //    if (result.Succeeded)
                //    {
                //        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                //        return RedirectToLocal(returnUrl);
                //    }
                //}
                //AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
