using StatsSA.eRecruitment.IManager.Institutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.Manager.GenericRepository;

namespace StatsSA.eRecruitment.Manager.Institutions
{
    public class InstitutionsManager : IInstitutionsManager
    {
        private eRecruitmentEntities _context;
        public InstitutionsManager(eRecruitmentEntities context)
        {
            _context = context;
        }

        public Institution AddInstitution(Institution institution)
        {
            return _context.Institutions.Add(institution);
        }

        public IEnumerable<Institution> GetAllInstitutions()
        {
            return _context.Institutions.ToList();
        }

        public Institution GetInstitutionById(int institutionId)
        {
            return _context.Institutions.FirstOrDefault(institution => institution.InstitutionId == institutionId);
        }
    }
}
