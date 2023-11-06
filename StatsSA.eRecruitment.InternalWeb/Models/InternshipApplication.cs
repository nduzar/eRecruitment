using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StatsSA.eRecruitment.InternalWeb.Models
{
    public class InternshipApplication
    {
        public ListOfApplication Application { get; set; }

        public string Programme { get; set; }

        public string Average { get; set; }
    }
}