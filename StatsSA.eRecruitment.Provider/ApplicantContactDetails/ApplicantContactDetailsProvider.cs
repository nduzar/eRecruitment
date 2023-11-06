using StatsSA.eRecruitment.IProvider.ApplicantContactDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.UnitOfWork;

namespace StatsSA.eRecruitment.Provider.ApplicantContactDetails
{
    public class ApplicantContactDetailsProvider : IApplicantContactDetailsProvider
    {
        private IUnitOfWork _unitOfWork;
        public ApplicantContactDetailsProvider(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public ApplicantContactDetail AddApplicantContactDetail(ApplicantContactDetail contactDetail)
        {
            var checkContact = this._unitOfWork.ApplicantContactDetailsManager.getApplicantContactDetail(contactDetail.ApplicantProfileId);
            if (checkContact == null)
            {
                this._unitOfWork.BeginTransaction();
                var applicantContactDetail = this._unitOfWork.ApplicantContactDetailsManager.AddApplicantContactDetails(contactDetail);
                this._unitOfWork.SaveChanges();
                this._unitOfWork.CommitTransaction();
                return applicantContactDetail;
            }
            else
            {
                checkContact.LanguageId = contactDetail.LanguageId;
                checkContact.CellNumber = contactDetail.CellNumber;
                checkContact.ContactMethodId = contactDetail.ContactMethodId;
                checkContact.ContactMethogDetails = contactDetail.ContactMethogDetails;
                checkContact.MaintUser = contactDetail.MaintUser;
                checkContact.MaintDate = contactDetail.MaintDate;

                this._unitOfWork.BeginTransaction();
                var applicantContactDetails = this._unitOfWork.ApplicantContactDetailsManager.UpdateContactDetail(checkContact);
                this._unitOfWork.SaveChanges();
                this._unitOfWork.CommitTransaction();
                return applicantContactDetails;
            }
        }
        public ApplicantContactDetail GetApplicantContactDetail(int applicantProfileId)
        {
            var applicantContactDetails = this._unitOfWork.ApplicantContactDetailsManager.getApplicantContactDetail(applicantProfileId);
            return applicantContactDetails;

        }
    }
}
