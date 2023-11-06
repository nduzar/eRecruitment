using StatsSA.eRecruitment.IProvider.VerApplicantDeclarations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.UnitOfWork;

namespace StatsSA.eRecruitment.Provider.VerApplicantDeclarations
{
    public class VerApplicantDeclarationProvider : IVerApplicantDeclarationProvider
    {
        private IUnitOfWork _unitOfWork;
        public VerApplicantDeclarationProvider(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public VerApplicantDeclaration GetVerApplicantDeclaration(int VerApplicantProfileId)
        {
            return this._unitOfWork.VerApplicantDeclarationManager.getVerApplicantDeclaration(VerApplicantProfileId);
        }
    }
}
