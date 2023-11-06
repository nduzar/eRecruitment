using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Hosting;
using System.Web.Http.WebHost;
using System.Web.Security;
using System.Web.SessionState;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace StatsSA.eRecruitment.WebApi
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {



            GlobalConfiguration.Configuration.Services.Replace(typeof(IHostBufferPolicySelector), new CustomWebHostBufferPolicySelector());

            //if (ReferenceEquals(null, HttpContext.Current.Request.Headers["Authorization"]))
            //{
            //    var token = HttpContext.Current.Request.Params["access_token"];
            //    if (!String.IsNullOrEmpty(token))
            //    {
            //        HttpContext.Current.Request.Headers.Add("Authorization", "Bearer " + token);
            //    }
            //}
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }

    public class CustomWebHostBufferPolicySelector : WebHostBufferPolicySelector
    {
        public override bool UseBufferedInputStream(object hostContext)
        {
            var context = hostContext as HttpContextBase;

            if (context != null)
            {
                if (context.Request.RequestContext.RouteData.Values != null && context.Request.RequestContext.RouteData.Values.Count > 0)
                {
                    if (string.Equals(context.Request.RequestContext.RouteData.Values["controller"].ToString(), "ApplicantProfile", StringComparison.InvariantCultureIgnoreCase))
                        return false;
                }
            }

            return true;
        }

        public override bool UseBufferedOutputStream(HttpResponseMessage response)
        {
            return base.UseBufferedOutputStream(response);
        }
    }
}