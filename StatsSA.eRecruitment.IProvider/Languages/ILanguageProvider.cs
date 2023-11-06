using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IProvider.Languages
{
    public interface ILanguageProvider
    {
        IEnumerable<Language> GetAllLanguages();
        Language GetLanguageByLanguageId(int languageId);
    }
}
