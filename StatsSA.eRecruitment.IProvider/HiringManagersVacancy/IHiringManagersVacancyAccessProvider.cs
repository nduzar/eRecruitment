using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IProvider.HiringManagersVacancy
{
    public interface IHiringManagersVacancyAccessProvider
    {
        IEnumerable<HiringManagersVacancyAccess> GetHiringManagersVacancyAccess();

        HiringManagersVacancyAccess InsertHiringManagersVacancyAccess(HiringManagersVacancyAccess hiringManagersVacancyAccess);

        HiringManagersVacancyAccess UpdateHiringManagersVacancyAccess(HiringManagersVacancyAccess hiringManagersVacancyAccess);
    }
}
