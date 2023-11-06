using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IProvider.VerApplicantLangProviciencies
{
    public interface IVerApplicantLangProviciencyProvider
    {
        IEnumerable<VerApplicantLangProficiency> GetVerApplicantProficiencies(int VerApplicantProfileId);

    }
}
