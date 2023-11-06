using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IManager.ApplicantDeclarations

{
    public interface IApplicantDeclarationManager
    {
        void AddApplicantDeclaration(Entities.ApplicantDeclaration declaration);

        Entities.ApplicantDeclaration getApplicantDeclaration(int ApplicantProfileId);

        Entities.ApplicantDeclaration UpdateApplicantDeclaration(Entities.ApplicantDeclaration declaration);

    }
 
}
