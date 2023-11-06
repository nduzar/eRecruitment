using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IManager.VerApplicantReferences

{
    public interface IVerApplicantReferencesManager
    {
        IEnumerable<VerApplicantReference> GetVerApplicantReferences(int VerApplicantProfileId);
    }
 
}
