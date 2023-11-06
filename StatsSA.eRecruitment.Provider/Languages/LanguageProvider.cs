using StatsSA.eRecruitment.IProvider.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.UnitOfWork;

namespace StatsSA.eRecruitment.Provider.Language
{
    public class LanguageProvider : ILanguageProvider
    {
        private IUnitOfWork _unitOfWork;
        public LanguageProvider(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Entities.Language> GetAllLanguages()
        {
            return _unitOfWork.LanguageManager.GetLanguages();
        }

        Entities.Language ILanguageProvider.GetLanguageByLanguageId(int languageId)
        {
            return _unitOfWork.LanguageManager.GetLanguageByLanguageId(languageId);
        }
    }
}
