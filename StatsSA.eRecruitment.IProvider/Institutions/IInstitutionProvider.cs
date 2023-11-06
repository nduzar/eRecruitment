using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IProvider.Institutions
{
    public interface IInstitutionProvider
    {
        Institution AddInstitution(Institution institution);

        IEnumerable<Institution> GetAllInstitutions();

        Institution GetInstitutionById(int institutionId);
    }
}
