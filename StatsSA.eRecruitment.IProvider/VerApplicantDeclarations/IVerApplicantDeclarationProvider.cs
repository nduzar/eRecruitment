using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IProvider.ApplicantDeclarations
{
    public interface IApplicantDeclarationProvider
    {
        Entities.ApplicantDeclaration AddApplicantDeclaration(Entities.ApplicantDeclaration declaration);

        Entities.ApplicantDeclaration GetApplicantDeclaration(int applicantProfileId);

    }
}
