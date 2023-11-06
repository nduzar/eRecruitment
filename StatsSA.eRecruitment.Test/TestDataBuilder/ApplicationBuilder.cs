using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.Test.TestDataBuilder
{
    public class ApplicationBuilder
    {
        private int ApplicationStatusId = 1;
        private DateTime ApplicationStatusDate = DateTime.Now;
        private string MaintUser = "TestUser";
        private DateTime MaintDate = DateTime.Now;

        public Application Build(ApplicantProfile applicantProfile, Vacancy vacancy)
        {
            return new Application
            {
                VacancyId = vacancy.VacancyId,
                VerApplicantProfileId = applicantProfile.ApplicantProfileId,
                ApplicationStatusId = this.ApplicationStatusId,
                ApplicationStatusDate = this.ApplicationStatusDate,
                MaintUser = this.MaintUser,
                MaintDate = this.MaintDate
            };
        }
    }
}
