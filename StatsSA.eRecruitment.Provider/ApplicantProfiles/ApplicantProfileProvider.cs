using StatsSA.eRecruitment.IProvider.ApplicantProfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.UnitOfWork;

namespace StatsSA.eRecruitment.Provider.ApplicantProfiles
{
    public class ApplicantProfileProvider : IApplicantProfileProvider
    {
        private IUnitOfWork _unitOfWork;
        public ApplicantProfileProvider(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public Entities.ApplicantProfile AddApplicantProfile(Entities.ApplicantProfile applicantProfile)
        {
            if(applicantProfile.Citizen == true)
            {
                //applicantProfile.Nationality = string.Empty;
                applicantProfile.WorkPermit = null;
            }
            var applicantCheck = this._unitOfWork.ApplicantProfileManager.GetApplicantProfileByUserId((int)applicantProfile.UserId);
            if(applicantCheck == null)
            {
                applicantProfile.MaintDate = DateTime.Now;
                this._unitOfWork.BeginTransaction();
                var applicant = this._unitOfWork.ApplicantProfileManager.AddApplicantProfile(applicantProfile);
                this._unitOfWork.SaveChanges();
                this._unitOfWork.CommitTransaction();
                return applicant;
            }
            else
            {
                applicantCheck.FirstNames = applicantProfile.FirstNames;
                applicantCheck.Surname = applicantProfile.Surname;
                applicantCheck.DateOfBirth = applicantProfile.DateOfBirth;
                applicantCheck.IdentityNumber = applicantProfile.IdentityNumber;
                applicantCheck.RaceId = applicantProfile.RaceId;
                applicantCheck.GenderId = applicantProfile.GenderId;
                applicantCheck.Disability = applicantProfile.Disability;
                applicantCheck.Citizen = applicantProfile.Citizen;
                applicantCheck.Nationality = applicantProfile.Nationality;
                applicantCheck.WorkPermit = applicantProfile.WorkPermit;
                applicantCheck.PreviousDismissal = applicantProfile.PreviousDismissal;
                applicantCheck.CriminalOffence = applicantProfile.CriminalOffence;
                applicantCheck.OccupationRegistrationDate = applicantProfile.OccupationRegistrationDate;
                applicantCheck.OccupationRegistrationDesc = applicantProfile.OccupationRegistrationDesc;
                applicantCheck.MaintUser = applicantProfile.MaintUser;
                applicantCheck.MaintDate = applicantProfile.MaintDate;
                applicantCheck.CriminalOffenceDesc = applicantProfile.CriminalOffenceDesc;
                applicantCheck.DisabilityDesc = applicantProfile.DisabilityDesc;
                applicantCheck.PreviousDismissedReason = applicantProfile.PreviousDismissedReason;
                applicantCheck.BusinessWithState = applicantProfile.BusinessWithState;
                applicantCheck.BusinessWithStateDesc = applicantProfile.BusinessWithStateDesc;
                applicantCheck.DischargedOrRetired = applicantProfile.DischargedOrRetired;
                applicantCheck.NumberOfExperiencePrivate = applicantProfile.NumberOfExperiencePrivate;
                applicantCheck.NumberOfExperiencePublic = applicantProfile.NumberOfExperiencePublic;
                applicantCheck.PendingCriminalCase = applicantProfile.PendingCriminalCase;
                applicantCheck.PendingCriminalDesc = applicantProfile.PendingCriminalDesc;
                applicantCheck.PendingDisciplinary = applicantProfile.PendingDisciplinary;
                applicantCheck.PendingDisciplinaryDesc = applicantProfile.PendingDisciplinaryDesc;
                applicantCheck.PublicServiceDismiss = applicantProfile.PublicServiceDismiss;
                applicantCheck.PublicServiceDismissDesc = applicantProfile.PublicServiceDismissDesc;
                applicantCheck.RelinquishBusinessInterest = applicantProfile.RelinquishBusinessInterest;
                applicantCheck.ResignedPendingDisciplinary = applicantProfile.ResignedPendingDisciplinary;
                applicantCheck.ResignedPendingDisciplinaryDesc = applicantProfile.ResignedPendingDisciplinaryDesc;

                this._unitOfWork.BeginTransaction();
                var applicant = this._unitOfWork.ApplicantProfileManager.UpdateApplicantProfile(applicantCheck);
                this._unitOfWork.SaveChanges();
                this._unitOfWork.CommitTransaction();
                return applicant;
            }
            
        }

        IEnumerable<ApplicantQualification> IApplicantProfileProvider.AddApplicantQualification(ApplicantQualification qualification)
        {
            this._unitOfWork.BeginTransaction();
            this._unitOfWork.ApplicantProfileManager.AddApplicantQualification(qualification);
            this._unitOfWork.SaveChanges();
            this._unitOfWork.CommitTransaction();
            var allQualifications = this._unitOfWork.ApplicantProfileManager.GetApplicantQualification(qualification.ApplicantProfileId);
            return allQualifications;
        }

        public Entities.ApplicantProfile GetApplicantProfileByIDNo(string idNumber)
        {
           return _unitOfWork.ApplicantProfileManager.GetApplicantProfileByIdNo(idNumber);
        }

        public Entities.ApplicantProfile GetApplicantProfileByUserId(int userId)
        {
           // this._unitOfWork.BeginTransaction();

            var applicantProfile = this._unitOfWork.ApplicantProfileManager.GetApplicantProfileByUserId(userId);

            //this._unitOfWork.CommitTransaction();

            return applicantProfile;
        }

        public Entities.ApplicantProfile UpdateApplicantProfile(Entities.ApplicantProfile applicantProfile)
        {
            this._unitOfWork.BeginTransaction();

            var applicant = this._unitOfWork.ApplicantProfileManager.UpdateApplicantProfile(applicantProfile);

            this._unitOfWork.CommitTransaction();

            return applicant;
        }

        public ApplicantProfile GetApplicantProfileProfileId(int profileId)
        {
            return _unitOfWork.ApplicantProfileManager.GetApplicantProfileProfileId(profileId);
        }



    }
}
