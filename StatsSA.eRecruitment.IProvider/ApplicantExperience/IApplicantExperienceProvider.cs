using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IProvider.ApplicantExperiences
{
    public interface IApplicantExperienceProvider
    {
        ApplicantExperience AddApplicantExperience(ApplicantExperience experience);
        ApplicantExperience UpdateApplicantExperience(ApplicantExperience experience);
        IEnumerable<ApplicantExperience> GetApplicantExperience(int applicantProfileId);
        void RemoveApplicantExperience(ApplicantExperience experience);

    }
}
