using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IProvider.VerApplicantExperiences
{
    public interface IVerApplicantExperienceProvider
    {
        IEnumerable<VerApplicantExperience> GetVerApplicantExperience(int VerApplicantProfileId);

    }
}
