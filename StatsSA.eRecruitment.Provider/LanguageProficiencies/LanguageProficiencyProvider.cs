using StatsSA.eRecruitment.IProvider.LanguageProficiencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.UnitOfWork;

namespace StatsSA.eRecruitment.Provider.LanguageProficiencies
{
    public class LanguageProficiencyProvider : ILanguageProficiencyProvider
    {
        private IUnitOfWork _unitOfWork;
        public LanguageProficiencyProvider(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<LanguageProficiency> GetLanguageProficiencies()
        {
            return _unitOfWork.LanguageProficiencyManager.GetLanguageProficiencies();
        }
        public LanguageProficiency GetLanguageProficiencyById(int LanguageProficiencyId)
        {
            return _unitOfWork.LanguageProficiencyManager.GetLanguageProficiencyById(LanguageProficiencyId);
        }
    }
}
