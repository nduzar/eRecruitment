using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IProvider.VerApplicantReferences
{
    public interface IVerApplicantReferenceProvider
    {
        IEnumerable<VerApplicantReference> GetVerApplicantReferences(int VerApplicantProfileId);
    }
}
