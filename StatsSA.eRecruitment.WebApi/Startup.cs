using System;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using StatsSA.eRecruitment.WebApi.Providers;
using StatsSA.eRecruitment.WebApi.Unity;

[assembly: OwinStartup(typeof(StatsSA.eRecruitment.WebApi.Startup))]
namespace StatsSA.eRecruitment.WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Bootstrapper.Initialise();
            ConfigureOAuth(app);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(7200),
                Provider = new ADAuthorizationServerProvider(),
                RefreshTokenProvider = new SimpleRefreshTokenProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }
    }   
}
