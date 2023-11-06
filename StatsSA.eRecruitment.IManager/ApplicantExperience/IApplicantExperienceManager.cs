using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IManager.Experiences

{
    public interface IApplicantExperienceManager
    {
        void AddApplicantExperience(ApplicantExperience experience);
        void UpdateApplicantExperience(ApplicantExperience experience);
        IEnumerable<ApplicantExperience> getApplicantExperience(int ApplicantProfileId);

        void removeApplicantExperience(int ApplicantExperienceId);

    }
 
}
