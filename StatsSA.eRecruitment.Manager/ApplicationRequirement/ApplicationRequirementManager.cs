using StatsSA.eRecruitment.IManager.Requirements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.Manager.GenericRepository;

namespace StatsSA.eRecruitment.Manager.ApplicationRequirements
{
    public class RequirementManager : IRequirementManager
    {
        private eRecruitmentEntities _context;
        public RequirementManager(eRecruitmentEntities context)
        {
            _context = context;
        }

        public IEnumerable<Requirement> GetAllRequirements()
        {
            return _context.Requirements;
        }

        public Requirement GetRequirementById(int ApplicationId)
        {
            return _context.Requirements.FirstOrDefault(aR => aR.ApplicationId == ApplicationId);
        }
    }
}
