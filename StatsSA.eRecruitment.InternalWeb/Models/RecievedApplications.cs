using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StatsSA.eRecruitment.InternalWeb.Models
{
    public class RecievedApplications
    {
        public Application ApplicantApplication { get; set; }
        public VerApplicantProfile Profile { get; set; }
        public int Score { get; set; }
        public bool hasAttachments { get; set; }
        public int NumberofApplications { get; set; }

        public ListOfApplication Application { get; set; }
       
    }
}