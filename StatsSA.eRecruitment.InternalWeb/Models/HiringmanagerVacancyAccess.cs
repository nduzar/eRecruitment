using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StatsSA.eRecruitment.InternalWeb.Models
{
    public class HiringmanagerVacancyAccess
    {
        public List<HRUser> Users { get; set; }

        public int VacancyId { get; set; }
    }
}