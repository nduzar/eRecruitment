using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IManager.VerApplicantDeclarations

{
    public interface IVerApplicantDeclarationManager
    {
        VerApplicantDeclaration getVerApplicantDeclaration(int verApplicantProfileId);


    }
 
}
