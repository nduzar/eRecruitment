using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.UnitOfWork;
using StatsSA.eRecruitment.IProvider.ApplicationStatuses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.Provider.ApplicationStatuses
{
    public class ApplicationStatusProvider : IApplicationStatusProvider
    {
        private IUnitOfWork _unitOfWork;
        public ApplicationStatusProvider(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ApplicationStatus GetApplicationStatus(int applicationStatusId)
        {
            return _unitOfWork.ApplicationStatusManager.GetApplicationStatus(applicationStatusId);
        }
    }
}
