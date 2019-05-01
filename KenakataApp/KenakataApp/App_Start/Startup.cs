using System;
using System.Threading.Tasks;
using System.Web.Http;
using KenakataApp.Server_Providers;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup(typeof(KenakataApp.App_Start.Startup))]

namespace KenakataApp.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            OAuthAuthorizationServerOptions option = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,

                TokenEndpointPath = new PathString("/token"),

               AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),

                Provider = new AdminAuthorizationServerProvider()

            };
           

            app.UseOAuthAuthorizationServer(option);
         
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
         
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);


        }

    }
}
