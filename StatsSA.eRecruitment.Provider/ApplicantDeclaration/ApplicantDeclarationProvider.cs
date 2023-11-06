using StatsSA.eRecruitment.IProvider.ApplicantDeclarations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.UnitOfWork;

namespace StatsSA.eRecruitment.Provider.ApplicantDeclarations
{
    public class ApplicantDeclarationProvider : IApplicantDeclarationProvider
    {
        private IUnitOfWork _unitOfWork;
        public ApplicantDeclarationProvider(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public Entities.ApplicantDeclaration AddApplicantDeclaration(Entities.ApplicantDeclaration declaration)
        {
            var checkDeclaration = this._unitOfWork.ApplicantDeclarationManager.getApplicantDeclaration(declaration.ApplicantProfileId);
            if (checkDeclaration == null)
            {
                this._unitOfWork.BeginTransaction();
                this._unitOfWork.ApplicantDeclarationManager.AddApplicantDeclaration(declaration);
                this._unitOfWork.SaveChanges();
                this._unitOfWork.CommitTransaction();
                var applicantDeclaration = this._unitOfWork.ApplicantDeclarationManager.getApplicantDeclaration(declaration.ApplicantProfileId);
                return applicantDeclaration;
            }
            else
            {
                checkDeclaration.AcceptedDeclaration = declaration.AcceptedDeclaration;
                checkDeclaration.AcceptedDeclarationOn = declaration.AcceptedDeclarationOn;
                checkDeclaration.MaintDate = declaration.MaintDate;
                checkDeclaration.MaintUser = checkDeclaration.MaintUser;

                this._unitOfWork.BeginTransaction();
                var applicantDeclaration = this._unitOfWork.ApplicantDeclarationManager.UpdateApplicantDeclaration(checkDeclaration);
                this._unitOfWork.SaveChanges();
                this._unitOfWork.CommitTransaction();
                return applicantDeclaration;
            }
        }

        public Entities.ApplicantDeclaration GetApplicantDeclaration(int applicantProfileId)
        {
            var applicantDeclaration = this._unitOfWork.ApplicantDeclarationManager.getApplicantDeclaration(applicantProfileId);
            return applicantDeclaration;
        }
    }
}
