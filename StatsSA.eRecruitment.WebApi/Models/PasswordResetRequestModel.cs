using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StatsSA.eRecruitment.WebApi.Models
{
    public class PasswordResetRequestModel
    {
        public SearchAccountResult SearchAccountResult { get; set; }
        public string ResertPasswordMethod { get; set; }
    }
}