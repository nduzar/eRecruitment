using StatsSA.eRecruitment.IProvider.UserClients;
using StatsSA.eRecruitment.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace StatsSA.eRecruitment.WebApi.Controllers
{
    [Authorize, RoutePrefix("api/userclient")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserClientController : ApiController
    {
        private IUserClientProvider _userClientProvider;
        public UserClientController(IUserClientProvider userClientProvider)
        {
            _userClientProvider = userClientProvider;
        }

        [HttpPost, Route("saveuserclient")]
        public IHttpActionResult SaveUserClient(UserClientRequest userClientRequest)
        {
            var result = _userClientProvider.SaveUserClient(userClientRequest.UserClientToAdd, userClientRequest.UserClientToRemove);
            return Ok(result);
        }
    }
}
