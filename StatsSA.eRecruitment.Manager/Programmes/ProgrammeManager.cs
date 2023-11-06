using StatsSA.eRecruitment.IManager.Genders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.Manager.GenericRepository;
using StatsSA.eRecruitment.IManager.Programmes;

namespace StatsSA.eRecruitment.Manager.Genders
{
    public class ProgrammeManager : IProgrammeManager
    {
        private eRecruitmentEntities _context;
        public ProgrammeManager(eRecruitmentEntities context)
        {
            _context = context;
        }

        public IEnumerable<Programme> GetAllProgrammes()
        {
            return _context.Programmes.ToList().Where(x => x.ProgrammeDesc != "NONE");
        }

        public Programme GetProgrammeById(int programmeId)
        {
            return _context.Programmes.FirstOrDefault(programme => programme.ProgrammeId == programmeId);
        }
    }
}
