using StatsSA.eRecruitment.IProvider.ApplicantExperiences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.UnitOfWork;

namespace StatsSA.eRecruitment.Provider.ApplicantExperiences
{
    public class ApplicantExperienceProvider : IApplicantExperienceProvider
    {
        private IUnitOfWork _unitOfWork;
        public ApplicantExperienceProvider(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public ApplicantExperience AddApplicantExperience(ApplicantExperience experience)
        {
            this._unitOfWork.BeginTransaction();
            this._unitOfWork.ApplicantExperienceManager.AddApplicantExperience(experience);
            this._unitOfWork.SaveChanges();
            this._unitOfWork.CommitTransaction();
            return experience;
        }
        public IEnumerable<ApplicantExperience> GetApplicantExperience(int applicantProfileId)
        {
            var applicantExperience = this._unitOfWork.ApplicantExperienceManager.getApplicantExperience(applicantProfileId);
            return applicantExperience;
        }

        public void RemoveApplicantExperience(ApplicantExperience experience)
        {
            this._unitOfWork.BeginTransaction();
            this._unitOfWork.ApplicantExperienceManager.removeApplicantExperience(experience.ApplicantExperienceId);
            this._unitOfWork.SaveChanges();
            this._unitOfWork.CommitTransaction();
        }

        public ApplicantExperience UpdateApplicantExperience(ApplicantExperience experience)
        {
            this._unitOfWork.BeginTransaction();
            this._unitOfWork.ApplicantExperienceManager.UpdateApplicantExperience(experience);
            this._unitOfWork.SaveChanges();
            this._unitOfWork.CommitTransaction();
            return experience;
        }
    }
}
