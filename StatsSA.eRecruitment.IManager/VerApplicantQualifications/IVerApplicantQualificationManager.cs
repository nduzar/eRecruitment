using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IManager.VerApplicantQualifications

{
    public interface IVerApplicantQualificationManager
    {
        IEnumerable<VerApplicantQualification> getVerApplicantQualification(int VerApplicantProfileId);
    }
}