using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.HiringManagersVacancy;
using StatsSA.eRecruitment.Manager.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.Manager.HiringManagersVacancy
{
    public class HiringManagersVacancyAccessManager : GenericRepository<HiringManagersVacancyAccess>, IHiringManagersVacancyAccessManager
    {
        private eRecruitmentEntities context;
        public HiringManagersVacancyAccessManager(eRecruitmentEntities context) : base(context)
        {
            this.context = context;
        }
        public IEnumerable<HiringManagersVacancyAccess> GetHiringManagersVacancyAccess()
        {
            return base.GetAll();
        }

        public HiringManagersVacancyAccess InsertHiringManagersVacancyAccess(HiringManagersVacancyAccess hiringManagersVacancyAccess)
        {
            base.Insert(hiringManagersVacancyAccess);
            context.SaveChanges();
            return hiringManagersVacancyAccess;
        }

        public HiringManagersVacancyAccess UpdateHiringManagersVacancyAccess(HiringManagersVacancyAccess hiringManagersVacancyAccess)
        {
            base.Update(hiringManagersVacancyAccess);
            context.SaveChanges();
            return hiringManagersVacancyAccess;                
        }
    }
}
