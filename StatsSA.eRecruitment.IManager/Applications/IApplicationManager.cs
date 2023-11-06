using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IManager.Applications
{
    public interface IApplicationManager
    {
        Application AddApplication(Application ApplicationToAdd);

        void UpdateApplication(Application ApplicationToUpdate);

        IEnumerable<Application> GetApplicantApplications(int applicantProfileId);

        Application GetApplicationById(int ApplicationId);

        IEnumerable<Application> GetRankedApplications(int vacancyId);

        IEnumerable<Application> GetApplicationByEmail(int vacancyId, string email);

        void UpdateApplicationStatus(Application ApplicationToUpdate);

        IEnumerable<Application> GetApplicantApplication(IEnumerable<int> verApplicantProfileIds);
        IEnumerable<ListOfApplication> GetAllApplications(int vacancyId);
    }
}
