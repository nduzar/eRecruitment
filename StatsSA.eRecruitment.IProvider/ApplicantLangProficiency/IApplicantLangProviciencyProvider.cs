using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IProvider.ApplicantLanguageProficiencies
{
    public interface IApplicantLangProviciencyProvider
    {
        ApplicantLangProficiency AddApplicantProficiency(ApplicantLangProficiency proficiency);
        ApplicantLangProficiency UpdateApplicantProficiency(ApplicantLangProficiency proficiency);
        IEnumerable<ApplicantLangProficiency> GetApplicantProficiencies(int applicantProfileId);
        void RemoveApplicantProficiency(ApplicantLangProficiency proficiency);

    }
}
