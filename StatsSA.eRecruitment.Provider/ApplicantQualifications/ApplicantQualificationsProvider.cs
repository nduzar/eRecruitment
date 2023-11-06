using StatsSA.eRecruitment.IProvider.ApplicantQualifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.UnitOfWork;

namespace StatsSA.eRecruitment.Provider.ApplicantQualifications
{
    public class ApplicantQualificationsProvider : IApplicantQualificationsProvider
    {
        private IUnitOfWork _unitOfWork;
        public ApplicantQualificationsProvider(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public ApplicantQualification AddApplicantQualification(ApplicantQualification qualification)
        {
            this._unitOfWork.BeginTransaction();
            this._unitOfWork.ApplicantQualificationManager.AddApplicantQualification(qualification);
            this._unitOfWork.SaveChanges();
            this._unitOfWork.CommitTransaction();            
            return qualification;
        }
        public IEnumerable<ApplicantQualification> GetApplicantQualification(int applicantProfileId)
        {
            var applicantQualification = this._unitOfWork.ApplicantQualificationManager.getApplicantQualification(applicantProfileId);
            return applicantQualification;
        }

        public void RemoveApplicantQualification(ApplicantQualification qualification)
        {
            this._unitOfWork.BeginTransaction();
            this._unitOfWork.ApplicantQualificationManager.RemoveApplicantQualification(qualification.ApplicantQualificationId);
            this._unitOfWork.SaveChanges();
            this._unitOfWork.CommitTransaction();
        }

        public ApplicantQualification UpdateApplicantQualification(ApplicantQualification qualification)
        {
            this._unitOfWork.BeginTransaction();
            this._unitOfWork.ApplicantQualificationManager.UpdateApplicantQualification(qualification);
            this._unitOfWork.SaveChanges();
            this._unitOfWork.CommitTransaction();
            return qualification;
        }
    }
}
