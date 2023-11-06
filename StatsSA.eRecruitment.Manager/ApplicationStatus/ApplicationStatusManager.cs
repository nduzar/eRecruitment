using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.Entities.Enums;
using StatsSA.eRecruitment.IManager.ApplicationStatuses;
using StatsSA.eRecruitment.Manager.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.Manager.ApplicationStatuses
{
    public class ApplcationStatusManager : IApplicationStatusManager
    {
        private eRecruitmentEntities _context;
        public ApplcationStatusManager(eRecruitmentEntities context)
        {
            _context = context;
        }

        public ApplicationStatus GetApplicationStatus(int applicationStatusId)
        {
            return _context.ApplicationStatuses.FirstOrDefault(applStatus => applStatus.ApplicationStatusId == applicationStatusId);
        }
    }
}
