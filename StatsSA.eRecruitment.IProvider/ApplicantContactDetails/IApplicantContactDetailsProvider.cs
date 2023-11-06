using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IProvider.ApplicantContactDetails
{
    public interface IApplicantContactDetailsProvider
    {
        ApplicantContactDetail AddApplicantContactDetail(ApplicantContactDetail contactDetail);
        ApplicantContactDetail GetApplicantContactDetail(int applicantProfileId);

    }
}
