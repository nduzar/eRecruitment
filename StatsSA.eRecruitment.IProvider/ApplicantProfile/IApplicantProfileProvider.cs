using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IProvider.ApplicantProfiles
{
    public interface IApplicantProfileProvider
    {
        ApplicantProfile AddApplicantProfile(ApplicantProfile applicantProfile);

        ApplicantProfile UpdateApplicantProfile(ApplicantProfile applicantProfile);

        ApplicantProfile GetApplicantProfileByIDNo(string idNumber);
        ApplicantProfile GetApplicantProfileByUserId(int userId);

        IEnumerable<ApplicantQualification> AddApplicantQualification(ApplicantQualification qualification);

        ApplicantProfile GetApplicantProfileProfileId(int profileId);



    }
}
