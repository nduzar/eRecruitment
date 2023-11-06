using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IManager.ApplicantContactDetails

{
    public interface IApplicantContactDetailsManager
    {
        ApplicantContactDetail AddApplicantContactDetails(ApplicantContactDetail contactDetail);

        ApplicantContactDetail getApplicantContactDetail(int ApplicantProfileId);
        ApplicantContactDetail UpdateContactDetail(ApplicantContactDetail contactDetail);

    }
 
}
