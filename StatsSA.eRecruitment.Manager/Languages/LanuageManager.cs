using StatsSA.eRecruitment.IManager.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.Manager.GenericRepository;

namespace StatsSA.eRecruitment.Manager.Languages
{
    public class LanguageManager : ILanguageManager
    {
        private eRecruitmentEntities _context;
        public LanguageManager(eRecruitmentEntities context)
        {
            _context = context;
        }

        public IEnumerable<Language> GetLanguages()
        {
            return _context.Languages.ToList();
        }
        public Language GetLanguageByLanguageId(int languageId)
        {
            return _context.Languages.FirstOrDefault(Lang => Lang.LanguageId == languageId);
        }
    }
}
