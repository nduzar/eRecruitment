using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IManager.ApplicantReferences

{
    public interface IApplicantReferenceManager
    {
        void AddApplicantReference(ApplicantReference reference);
        void UpdateApplicantReference(ApplicantReference reference);
        IEnumerable<ApplicantReference> GetApplicantReferences(int ApplicantProfileId);

        void RemoveApplicantReferences(int ApplicantReferenceId);
    }
 
}
