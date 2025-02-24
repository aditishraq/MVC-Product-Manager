using System;
using Owin;
using Microsoft.Owin; // Required for IAppBuilder
// using Microsoft.AspNet.Identity; // Comment out if not using Identity
// using Microsoft.AspNet.Identity.Owin; // Comment out if not using Identity
// using Microsoft.Owin.Security.Cookies; // Comment out if not using Identity
// using MvcApp.Models; // Comment out if not using any Identity models

[assembly: OwinStartup(typeof(MvcApp.Startup))]
namespace MvcApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // If you're rolling back from ASP.NET Identity,
            // simply remove or comment out the code below:

            /*
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // This logic validates the security stamp for users who are logged in.
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            */

            // You can remove the above lines entirely if you do not want any authentication in your app.
        }
    }
}
