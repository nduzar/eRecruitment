using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IProvider.ApplicantQualifications
{
    public interface IApplicantQualificationsProvider
    {
        ApplicantQualification AddApplicantQualification(ApplicantQualification qualification);
        ApplicantQualification UpdateApplicantQualification(ApplicantQualification qualification);
        IEnumerable<ApplicantQualification> GetApplicantQualification(int applicantProfileId);
        void RemoveApplicantQualification(ApplicantQualification qualification);
    }
}
