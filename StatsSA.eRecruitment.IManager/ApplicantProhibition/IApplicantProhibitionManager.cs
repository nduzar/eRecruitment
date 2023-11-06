using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IManager.ApplicantProhibition

{
    public interface IApplicantProhibitionManager
    {
        void AddApplicantProhibition(ApplicantPubServProhibition prohibition);

        ApplicantPubServProhibition getApplicantProhibition(int ApplicantProfileId);

        ApplicantPubServProhibition UpdateApplicantProhibition(ApplicantPubServProhibition prohibition);

    }
 
}
