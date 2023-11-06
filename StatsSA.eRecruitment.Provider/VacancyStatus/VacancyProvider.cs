using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.UnitOfWork;
using StatsSA.eRecruitment.IProvider.VacancyStatuses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.Provider.VacancyStatuses
{
    public class VacancyStatusProvider : IVacancyStatusProvider
    {
        private IUnitOfWork _unitOfWork;
        public VacancyStatusProvider(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public VacancyStatus GetVacancyStatus(int vacancyStatusId)
        {
            return _unitOfWork.VacancyStatusManager.GetVacancyStatus(vacancyStatusId);
        }

        public IEnumerable<VacancyStatus> GetVacancyStatuses()
        {
            return _unitOfWork.VacancyStatusManager.GetVacancyStatuses();
            
        }
    }
}
