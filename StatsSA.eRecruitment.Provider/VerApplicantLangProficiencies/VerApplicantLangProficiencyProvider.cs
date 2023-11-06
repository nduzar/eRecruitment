using StatsSA.eRecruitment.IProvider.VerApplicantLangProviciencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.UnitOfWork;

namespace StatsSA.eRecruitment.Provider.VerApplicantLangProviciencies
{
    public class VerApplicantLangProficiencyProvider : IVerApplicantLangProviciencyProvider
    {
        private IUnitOfWork _unitOfWork;
        public VerApplicantLangProficiencyProvider(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public IEnumerable<VerApplicantLangProficiency> GetVerApplicantProficiencies(int VerApplicantProfileId)
        {
            return this._unitOfWork.VerApplicantLangProficiencyManager.GetVerApplicantProficiencies(VerApplicantProfileId);
        }
    }
}
