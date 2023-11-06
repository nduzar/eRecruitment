using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.Entities.Enums;
using StatsSA.eRecruitment.IManager.VacancyStatuses;
using StatsSA.eRecruitment.Manager.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.Manager.VacancyStatuses
{
    public class VacancyStatusManager : IVacancyStatusManager
    {
        private eRecruitmentEntities _context;
        public VacancyStatusManager(eRecruitmentEntities context)
        {
            _context = context;
        }

        public VacancyStatus GetVacancyStatus(int vacancyStatusId)
        {
            return _context.VacancyStatuses.FirstOrDefault(vStatus => vStatus.VacancyStatusId == vacancyStatusId);
        }

        public IEnumerable<VacancyStatus> GetVacancyStatuses()
        {
            return _context.VacancyStatuses.ToList();
        }
    }
}
