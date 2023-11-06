using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StatsSA.eRecruitment.Entities;

namespace StatsSA.eRecruitment.InternalWeb.Models
{
    public class RankedApplication
    {
        public Application ApplicantApplication { get; set; }
        public VerApplicantProfile Profile { get; set; }

        public List<viewApplicantQuestionAnswer> AllApplicantQuestionAnswers { get; set; }

    }
}