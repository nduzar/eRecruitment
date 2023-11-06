using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IProvider.Roles;
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
    [Authorize, RoutePrefix("api/role")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RoleController : ApiController
    {
        private IRoleProvider _roleProvider;
        public RoleController(IRoleProvider roleProvider)
        {
            _roleProvider = roleProvider;
        }

        [HttpPost, Route("addrole")]
        public IHttpActionResult AddRole(Role role)
        {
            var result = _roleProvider.AddRole(role);
            return Ok(result);
        }

        [HttpPost, Route("getroles")]
        public IHttpActionResult GetRoles()
        {
            var result = _roleProvider.GetRoles();
            return Ok(result);
        }

        [HttpPost, Route("rolesbyclientid")]
        public IHttpActionResult RolesByClientId(RoleRequest reuest)
        {
            var result = _roleProvider.RolesByClientId(reuest.ClientId);
            return Ok(result);
        }
    }
}
