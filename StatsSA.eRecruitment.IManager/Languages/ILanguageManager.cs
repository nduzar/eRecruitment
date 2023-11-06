using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IManager.Languages
{
    public interface ILanguageManager
    {
        IEnumerable<Language> GetLanguages();
        Language GetLanguageByLanguageId(int languageId);
    }
}
