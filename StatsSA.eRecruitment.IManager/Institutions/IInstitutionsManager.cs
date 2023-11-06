using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IManager.Institutions
{
    public interface IInstitutionsManager
    {
        Institution AddInstitution(Institution institution);

        IEnumerable<Institution> GetAllInstitutions();

        Institution GetInstitutionById(int institutionId);
    }
}
