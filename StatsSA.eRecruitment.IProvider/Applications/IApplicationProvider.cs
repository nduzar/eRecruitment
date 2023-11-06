using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IProvider.Applications
{
    public interface IApplicationProvider
    {
        Application AddApplication(Application ApplicationToAdd);
        void UpdateApplication(Application ApplicationToUpdate);
        IEnumerable<Application> GetApplicantApplication(int userId);
        IEnumerable<Application> GetRankedApplications(int vacancyId);
        IEnumerable<Application> GetApplicationByEmail(int vacancyId, string email);

        Requirement GetRequirement(int applicationID);
        void UpdateApplicationStatus(Application ApplicationToUpdate);

        IEnumerable<Application>  GetApplicantApplication(IEnumerable<int> verApplicantProfileIds);
        IEnumerable<ListOfApplication> GetAllApplications(int vacancyId);
        Application GetApplicantApplication(int vacancyId, int applicationId);
    }
}
