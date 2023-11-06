using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StatsSA.eRecruitment.WebApi.Models
{
    public class ClientRoleRequest
    {
        public IEnumerable<ClientRole> ClientRoleToAdd { get; set; }
        public IEnumerable<ClientRole> ClientRoleToRemove { get; set; }
    }
}