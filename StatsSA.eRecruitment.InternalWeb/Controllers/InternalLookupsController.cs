using StatsSA.eRecruitment.IProvider.Salaries;
using StatsSA.eRecruitment.InternalWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using StatsSA.eRecruitment.IProvider.HRRoles;
using StatsSA.eRecruitment.Entities;

namespace StatsSA.eRecruitment.InternalWeb.Controllers
{
    [RoutePrefix("api/lookups")]
    public class InternalLookupsController : ApiController
    {
        private ISalariesProvider _SalaryProvider;
        private IHRRoleProvider _hrRolesProvider;

        public InternalLookupsController(ISalariesProvider salaryProvider, IHRRoleProvider hrRolesProvider)
        {
            _SalaryProvider = salaryProvider;
            _hrRolesProvider = hrRolesProvider;
        }
        [HttpPost, Route("getsalaries")]

        public IHttpActionResult GetSalaries()
        {
            var salaries = _SalaryProvider.GetAllSalaries();
            return Ok(salaries);
        }

        [HttpPost, Route("savesalary")]
        public IHttpActionResult SaveSalary(Salary salary)
        {
            _SalaryProvider.updateSalary(salary);
            return Ok(salary);
        }

        [HttpPost, Route("gethrroles")]
        public IHttpActionResult GetHRRoles()
        {
            var roles = this._hrRolesProvider.GetAllHRRoles();
            return Ok(roles);
        }

    }
}
