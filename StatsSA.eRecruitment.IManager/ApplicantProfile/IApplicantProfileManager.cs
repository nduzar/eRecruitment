using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IManager.Profiles

{

    public interface IApplicantProfileManager
    {
        ApplicantProfile AddApplicantProfile(ApplicantProfile profile);

       ApplicantProfile GetApplicantProfileByIdNo(string idNumber);

       ApplicantProfile UpdateApplicantProfileByIdNo(ApplicantProfile appliacantProfile);

        ApplicantProfile GetApplicantProfileByUserId(int userId);

        ApplicantProfile UpdateApplicantProfile(ApplicantProfile appliacantProfile);

        void AddApplicantQualification(ApplicantQualification qualification);

        IEnumerable<ApplicantQualification> GetApplicantQualification(int ApplicantProfileId);

        int GetProfileIdByUserId(int userId);

        bool CheckIfIdNumberExists(string IdNumber);

        ApplicantProfile GetApplicantProfileProfileId(int profileId);

    }
 
}
