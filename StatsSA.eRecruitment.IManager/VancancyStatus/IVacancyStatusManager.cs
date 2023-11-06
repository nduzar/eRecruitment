using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IManager.VacancyStatuses
{
    public interface IVacancyStatusManager
    {
        VacancyStatus GetVacancyStatus(int vacancyStatusId);
        IEnumerable<VacancyStatus> GetVacancyStatuses();
    }
}
