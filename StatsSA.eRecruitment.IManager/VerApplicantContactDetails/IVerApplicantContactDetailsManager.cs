using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IManager.VerApplicantContactDetails

{
    public interface IVerApplicantContactDetailsManager
    {
        VerApplicantContactDetail getVerApplicantContactDetail(int verApplicantProfileId);

    }
 
}
