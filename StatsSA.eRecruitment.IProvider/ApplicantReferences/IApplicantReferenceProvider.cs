using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IProvider.ApplicantReferences
{
    public interface IApplicantReferenceProvider
    {
        ApplicantReference AddApplicantReference(ApplicantReference reference);
        ApplicantReference UpdateApplicantReference(ApplicantReference reference);
        IEnumerable<ApplicantReference> GetApplicantReferences(int applicantProfileId);
        void RemoveApplicantReference(ApplicantReference reference);

    }
}
