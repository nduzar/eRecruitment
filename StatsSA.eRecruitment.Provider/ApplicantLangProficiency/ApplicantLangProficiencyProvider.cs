using StatsSA.eRecruitment.IProvider.ApplicantLanguageProficiencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.UnitOfWork;

namespace StatsSA.eRecruitment.Provider.ApplicantLanguageProficiencies
{
    public class ApplicantLangProficiencyProvider : IApplicantLangProviciencyProvider
    {
        private IUnitOfWork _unitOfWork;
        public ApplicantLangProficiencyProvider(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public ApplicantLangProficiency AddApplicantProficiency(ApplicantLangProficiency proficiency)
        {
            var isLanguageAlreadyExist = false;
            

            var applicantProficiencies = this._unitOfWork.ApplicantLangProficiencyManager.GetApplicantProficiencies(proficiency.ApplicantProfileId);

            isLanguageAlreadyExist = applicantProficiencies.Any(lp => lp.LanguageId == proficiency.LanguageId);

            if (isLanguageAlreadyExist)
            {
                Exception ex = new Exception("ProfileId: "+proficiency.ApplicantProfileId.ToString() + ", can not add language '"+ proficiency.LanguageId.ToString()+"' it already exist in your profile.",new Exception("ProfileId: " + proficiency.ApplicantProfileId.ToString() + "LanguageId" + proficiency.LanguageId.ToString() + ":: DUPLICATE_LANGUAGEPROFICIENCIES"));
                throw ex;
            }
            if (!isLanguageAlreadyExist)
            {
                this._unitOfWork.BeginTransaction();
                var applProf = this._unitOfWork.ApplicantLangProficiencyManager.AddApplicantProficiency(proficiency);
                this._unitOfWork.SaveChanges();
                this._unitOfWork.CommitTransaction();
                return proficiency;
            }
            return null;            
        }

        public IEnumerable<ApplicantLangProficiency> GetApplicantProficiencies(int applicantProfileId)
        {
            var applicantProficiencies = this._unitOfWork.ApplicantLangProficiencyManager.GetApplicantProficiencies(applicantProfileId);
            return applicantProficiencies;
        }

        public void RemoveApplicantProficiency(ApplicantLangProficiency proficiency)
        {
            this._unitOfWork.BeginTransaction();
            this._unitOfWork.ApplicantLangProficiencyManager.RemoveApplicantProficiency(proficiency.ApplicantLangProficiencyId);
            this._unitOfWork.SaveChanges();
            this._unitOfWork.CommitTransaction();
        }

        public ApplicantLangProficiency UpdateApplicantProficiency(ApplicantLangProficiency proficiency)
        {
            this._unitOfWork.BeginTransaction();
            _unitOfWork.ApplicantLangProficiencyManager.UpdateApplicantProficiency(proficiency);
            this._unitOfWork.SaveChanges();
            this._unitOfWork.CommitTransaction();
            return proficiency;
        }
    }
}
