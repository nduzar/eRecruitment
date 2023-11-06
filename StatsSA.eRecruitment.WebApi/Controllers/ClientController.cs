using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IProvider.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace StatsSA.eRecruitment.WebApi.Controllers
{
    [Authorize, RoutePrefix("api/client")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ClientController : ApiController
    {
        private IClientProvider _clientProvider;
        public ClientController(IClientProvider clientProvider)
        {
            _clientProvider = clientProvider;
        }

        [HttpPost, Route("addclient")]
        public IHttpActionResult AddClient(Client client)
        {
            var result = _clientProvider.AddClient(client);
            return Ok(result);
        }

        [HttpPost, Route("getclients")]
        public IHttpActionResult GetClients()
        {
            var result = _clientProvider.GetClients();
            return Ok(result);
        }
    }
}
