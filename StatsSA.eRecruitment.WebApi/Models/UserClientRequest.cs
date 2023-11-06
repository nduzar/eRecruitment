using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StatsSA.eRecruitment.WebApi.Models
{
    public class UserClientRequest
    {
        public IEnumerable<UserClient> UserClientToAdd { get; set; }
        public IEnumerable<UserClient> UserClientToRemove { get; set; }
    }
}