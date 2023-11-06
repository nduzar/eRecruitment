using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StatsSA.eRecruitment.WebApi.Models
{
    public class SearchAccountRequest
    {
        public string IdentityNumber { get; set; }
    }
    public class SearchAccountRequestByEmail
    {
        public string Email { get; set; }
    }
    
}