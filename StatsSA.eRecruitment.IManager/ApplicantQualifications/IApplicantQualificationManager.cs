using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IManager.Qualifications

{
    public interface IApplicantQualificationManager
    {
        void AddApplicantQualification(ApplicantQualification qualification);
        void UpdateApplicantQualification(ApplicantQualification qualification);
        IEnumerable<ApplicantQualification> getApplicantQualification(int ApplicantProfileId);

        void RemoveApplicantQualification(int ApplicantQualificationId);

    }
}