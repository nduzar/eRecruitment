using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IManager.ApplicantLanguageProficiencies

{
    public interface IApplicantLangProficiencyManager
    {
        ApplicantLangProficiency AddApplicantProficiency(ApplicantLangProficiency proficiency);
        void UpdateApplicantProficiency(ApplicantLangProficiency proficiency);
        IEnumerable<ApplicantLangProficiency> GetApplicantProficiencies(int ApplicantProfileId);
        void RemoveApplicantProficiency(int ApplicantLangProficiencyId);
    }
 
}
