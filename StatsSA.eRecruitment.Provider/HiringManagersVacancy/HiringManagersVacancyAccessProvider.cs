using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.UnitOfWork;
using StatsSA.eRecruitment.IProvider.HiringManagersVacancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.Provider.HiringManagersVacancy
{
    public class HiringManagersVacancyAccessProvider : IHiringManagersVacancyAccessProvider
    {
        private IUnitOfWork _unitOfWork;

        public HiringManagersVacancyAccessProvider(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<HiringManagersVacancyAccess> GetHiringManagersVacancyAccess()
        {
            return _unitOfWork.HiringManagersVacancyAccessManager.GetHiringManagersVacancyAccess();
        }

        public HiringManagersVacancyAccess InsertHiringManagersVacancyAccess(HiringManagersVacancyAccess hiringManagersVacancyAccess)
        {
            return _unitOfWork.HiringManagersVacancyAccessManager.InsertHiringManagersVacancyAccess(hiringManagersVacancyAccess);
        }

        public HiringManagersVacancyAccess UpdateHiringManagersVacancyAccess(HiringManagersVacancyAccess hiringManagersVacancyAccess)
        {
            return _unitOfWork.HiringManagersVacancyAccessManager.UpdateHiringManagersVacancyAccess(hiringManagersVacancyAccess);
        }
    }
}
