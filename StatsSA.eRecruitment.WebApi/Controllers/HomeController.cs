using StatsSA.eRecruitment.IProvider.Clients;
using StatsSA.eRecruitment.IProvider.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace StatsSA.eRecruitment.WebApi.Controllers
{
    [RoutePrefix("api/home")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class HomeController : ApiController
    {
        private IUserProvider _userProvider;
        private IClientProvider _clientProvider;
        public HomeController(IUserProvider userProvider, IClientProvider clientProvider)
        {
            _userProvider = userProvider;
            _clientProvider = clientProvider;
        }

        [Route("gethomedata")]
        public IHttpActionResult GetHomeData()
        {
            var userCount = _userProvider.GetAllUsers().ToList().Count();
            var clientCount = _clientProvider.GetClients().ToList().Count;
            var roleCount = 0;
            return Ok(new
            {
                UserCount = userCount,
                ClientCount = clientCount,
                RoleCount = roleCount
            });
        }
    }
}
