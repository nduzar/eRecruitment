using StatsSA.eRecruitment.IProvider.VerApplicantExperiences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.UnitOfWork;

namespace StatsSA.eRecruitment.Provider.VerApplicantExperiences
{
    public class VerApplicantExperienceProvider : IVerApplicantExperienceProvider
    {
        private IUnitOfWork _unitOfWork;
        public VerApplicantExperienceProvider(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public IEnumerable<VerApplicantExperience> GetVerApplicantExperience(int VerApplicantProfileId)
        {
            return this._unitOfWork.VerApplicantExperienceManager.getApplicantExperience(VerApplicantProfileId);
        }
    }
}