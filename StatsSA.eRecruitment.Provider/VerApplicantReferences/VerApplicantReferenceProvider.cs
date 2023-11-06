using StatsSA.eRecruitment.IProvider.VerApplicantReferences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.UnitOfWork;

namespace StatsSA.eRecruitment.Provider.VerApplicantReferences
{
    public class VerApplicantReferenceProvider : IVerApplicantReferenceProvider
    {
        private IUnitOfWork _unitOfWork;
        public VerApplicantReferenceProvider(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public IEnumerable<VerApplicantReference> GetVerApplicantReferences(int VerApplicantProfileId)
        {
            return this._unitOfWork.VerApplicantReferencesManager.GetVerApplicantReferences(VerApplicantProfileId);
        }
    }
}
