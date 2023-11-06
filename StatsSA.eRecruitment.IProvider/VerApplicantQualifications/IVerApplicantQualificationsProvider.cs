using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IProvider.VerApplicantQualifications
{
    public interface IVerApplicantQualificationsProvider
    {
        IEnumerable<VerApplicantQualification> GetVerApplicantQualification(int VerApplicantProfileId);
    }
}
