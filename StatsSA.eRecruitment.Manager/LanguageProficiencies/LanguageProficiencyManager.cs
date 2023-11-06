using StatsSA.eRecruitment.IManager.LanguageProficiencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.Manager.GenericRepository;

namespace StatsSA.eRecruitment.Manager.LanguageProficiencies
{
    public class LanguageProficiencyManager : ILanguageProficiencyManager
    {
        private eRecruitmentEntities _context;
        public LanguageProficiencyManager(eRecruitmentEntities context)
        {
            _context = context;
        }

        public IEnumerable<LanguageProficiency> GetLanguageProficiencies()
        {
            return _context.LanguageProficiencies.ToList();
        }
        public LanguageProficiency GetLanguageProficiencyById(int LanguageProficiencyId)
        {
            return _context.LanguageProficiencies.FirstOrDefault(lp => lp.LanguageProficiencyId == LanguageProficiencyId);
        }
    }
}
