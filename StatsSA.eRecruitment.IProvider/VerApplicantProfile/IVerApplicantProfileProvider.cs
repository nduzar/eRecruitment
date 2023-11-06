using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IProvider.VerApplicantProfiles
{
    public interface IVerApplicantProfileProvider
    {
        VerApplicantProfile GetVerAppProfileById(int VerApplicantProfileId);
        IEnumerable<VerApplicantProfile> GetVerAppProfilesById(int ApplicantProfileId);
        IEnumerable<VerApplicantProfile> GetVerAppProfileByUserId(int userId);
    }
}
