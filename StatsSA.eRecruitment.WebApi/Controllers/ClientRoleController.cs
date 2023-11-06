using StatsSA.eRecruitment.WebApi.Models;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IProvider.ClientRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace StatsSA.eRecruitment.WebApi.Controllers
{
    [RoutePrefix("api/clientrole")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ClientRoleController : ApiController
    {
        private IClientRoleProvider _clientRoleProvider;
        public ClientRoleController(IClientRoleProvider clientRoleProvider)
        {
            _clientRoleProvider = clientRoleProvider;
        }

        [HttpPost, Route("saveclientrole")]
        public IHttpActionResult AddClientRole(ClientRoleRequest request)
        {
            var result = _clientRoleProvider.SaveClientRole(request.ClientRoleToAdd,request.ClientRoleToRemove);
            return Ok(result);
        }
    }
}
