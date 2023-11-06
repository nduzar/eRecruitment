using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IManager.LanguageProficiencies
{
    public interface ILanguageProficiencyManager
    {
        IEnumerable<LanguageProficiency> GetLanguageProficiencies();
        LanguageProficiency GetLanguageProficiencyById(int LanguageProficiencyId);
    }
}
