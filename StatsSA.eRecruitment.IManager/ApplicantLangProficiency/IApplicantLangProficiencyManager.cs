using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IManager.VerApplicantLangProficiencies

{
    public interface IVerApplicantLangProficiencyManager
    {
        IEnumerable<VerApplicantLangProficiency> GetVerApplicantProficiencies(int VerApplicantProfileId);
    }
 
}
