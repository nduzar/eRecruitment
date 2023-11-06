using StatsSA.eRecruitment.IProvider.ApplicantProhibition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.UnitOfWork;

namespace StatsSA.eRecruitment.Provider.ApplicantProhibition
{
    public class ApplicantProhibitionProvider : IApplicantProhibitionProvider
    {
        private IUnitOfWork _unitOfWork;
        public ApplicantProhibitionProvider(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public ApplicantPubServProhibition AddApplicantProhibition(ApplicantPubServProhibition prohibition)
        {
            var checkProhibition = this._unitOfWork.ApplicantProhibitionManager.getApplicantProhibition(prohibition.ApplicantProfileId);
            if(checkProhibition == null)
            {
                this._unitOfWork.BeginTransaction();
                this._unitOfWork.ApplicantProhibitionManager.AddApplicantProhibition(prohibition);
                this._unitOfWork.SaveChanges();
                this._unitOfWork.CommitTransaction();
                var applicantProhibition = this._unitOfWork.ApplicantProhibitionManager.getApplicantProhibition(prohibition.ApplicantProfileId);
                return applicantProhibition;
            }
            else
            {
                checkProhibition.DepartmentName = prohibition.DepartmentName;
                checkProhibition.PublicServiceProhibition = prohibition.PublicServiceProhibition;
                checkProhibition.MaintUser = prohibition.MaintUser;
                checkProhibition.MaintDate = prohibition.MaintDate;

                this._unitOfWork.BeginTransaction();
                var applicantProhibition = this._unitOfWork.ApplicantProhibitionManager.UpdateApplicantProhibition(checkProhibition);
                this._unitOfWork.SaveChanges();
                this._unitOfWork.CommitTransaction();
                return applicantProhibition;
            }
        }


        public ApplicantPubServProhibition GetApplicantProhibition(int applicantProfileId)
        {
            var applicantProhibition = this._unitOfWork.ApplicantProhibitionManager.getApplicantProhibition(applicantProfileId);
            return applicantProhibition;
        }
    }
}
