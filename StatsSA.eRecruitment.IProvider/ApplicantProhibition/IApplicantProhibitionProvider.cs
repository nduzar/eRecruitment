using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IProvider.ApplicantProhibition
{
    public interface IApplicantProhibitionProvider
    {
        ApplicantPubServProhibition AddApplicantProhibition(ApplicantPubServProhibition prohibition);
        ApplicantPubServProhibition GetApplicantProhibition(int applicantProfileId);

    }
}
