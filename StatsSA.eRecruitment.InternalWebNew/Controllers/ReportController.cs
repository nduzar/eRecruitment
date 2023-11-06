using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StatsSA.eRecruitment.InternalWeb.Controllers
{
    public class ReportController : ApiController
    {
        public ReportController()
        {
        }
        [Route("api/Report/get")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            string baseUrl = ConfigurationManager.AppSettings["ReportServerURL"];
            return Ok(baseUrl);
        }
    }
}
