using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.MicrosoftAccount;
using Owin;

[assembly: OwinStartupAttribute(typeof(CC.UI.Webhost.Startup))]
namespace CC.UI.Webhost
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/LogOn")
            });


            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            var googleClientId = string.Empty;
            var googleSecret = string.Empty;
            var microsoftClientId = string.Empty;
            var microsoftSecret = string.Empty;


            googleClientId = System.Configuration.ConfigurationManager.AppSettings["GoogleOAuthClientId"];
            googleSecret = System.Configuration.ConfigurationManager.AppSettings["GoogleOAuthClientSecret"];
            microsoftClientId = System.Configuration.ConfigurationManager.AppSettings["MicrosoftOAuthClientId"];
            microsoftSecret = System.Configuration.ConfigurationManager.AppSettings["MicrosoftOAuthClientSecret"];

            // https://console.developers.google.com/apis
            if(!String.IsNullOrEmpty(googleClientId) & !String.IsNullOrEmpty(googleSecret))
            {
                app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
                {
                    ClientId = googleClientId,
                    ClientSecret = googleSecret
                });
            }
            
            // https://apps.dev.microsoft.com
            if (!String.IsNullOrEmpty(microsoftClientId) & !String.IsNullOrEmpty(microsoftSecret))
            {
                app.UseMicrosoftAccountAuthentication(new MicrosoftAccountAuthenticationOptions()
                {
                    ClientId = microsoftClientId,
                    ClientSecret = microsoftSecret,
                    Scope = { "wl.basic", "wl.emails" }
                });
            }

            
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
        }
    }
}