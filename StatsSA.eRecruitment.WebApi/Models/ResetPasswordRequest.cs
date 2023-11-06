using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StatsSA.eRecruitment.WebApi.Models
{
    public class ResetPasswordRequest
    {
        public string NewPassword { get; set; }
        public int UserId { get; set; }
    }
}