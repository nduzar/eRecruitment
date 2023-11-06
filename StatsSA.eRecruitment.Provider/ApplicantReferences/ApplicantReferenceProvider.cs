using StatsSA.eRecruitment.IProvider.ApplicantReferences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.UnitOfWork;

namespace StatsSA.eRecruitment.Provider.ApplicantReferences
{
    public class ApplicantReferenceProvider : IApplicantReferenceProvider
    {
        private IUnitOfWork _unitOfWork;
        public ApplicantReferenceProvider(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }


        public ApplicantReference AddApplicantReference(ApplicantReference reference)
        {
            this._unitOfWork.BeginTransaction();
            this._unitOfWork.ApplicantReferenceManager.AddApplicantReference(reference);
            this._unitOfWork.SaveChanges();
            this._unitOfWork.CommitTransaction();            
            return reference;
        }

        public IEnumerable<ApplicantReference> GetApplicantReferences(int applicantProfileId)
        {
            var applicantReferences = this._unitOfWork.ApplicantReferenceManager.GetApplicantReferences(applicantProfileId);
            return applicantReferences;
        }

        public void RemoveApplicantReference(ApplicantReference reference)
        {
            this._unitOfWork.BeginTransaction();
            this._unitOfWork.ApplicantReferenceManager.RemoveApplicantReferences(reference.ApplicantReferenceId);
            this._unitOfWork.SaveChanges();
            this._unitOfWork.CommitTransaction();
        }

        public ApplicantReference UpdateApplicantReference(ApplicantReference reference)
        {
            this._unitOfWork.BeginTransaction();
            this._unitOfWork.ApplicantReferenceManager.UpdateApplicantReference(reference);
            this._unitOfWork.SaveChanges();
            this._unitOfWork.CommitTransaction();            
            return reference;
        }
    }
}
