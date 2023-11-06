using StatsSA.eRecruitment.IProvider.VerApplicantProfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.UnitOfWork;

namespace StatsSA.eRecruitment.Provider.VerApplicantProfiles
{
    public class VerApplicantProfileProvider : IVerApplicantProfileProvider
    {
        private IUnitOfWork _unitOfWork;
        public VerApplicantProfileProvider(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public VerApplicantProfile GetVerAppProfileById(int VerApplicantProfileId)
        {
            return this._unitOfWork.VerApplicantProfileManager.GetVerAppProfileById(VerApplicantProfileId);
        }

        public IEnumerable<VerApplicantProfile> GetVerAppProfileByUserId(int userId)
        {
            return this._unitOfWork.VerApplicantProfileManager.GetVerAppProfileByUserId(userId);
        }

        public IEnumerable<VerApplicantProfile> GetVerAppProfilesById(int ApplicantProfileId)
        {
            var profiles = this._unitOfWork.VerApplicantProfileManager.GetVerAppProfilesById(ApplicantProfileId);
            return profiles;
        }
    }
}
