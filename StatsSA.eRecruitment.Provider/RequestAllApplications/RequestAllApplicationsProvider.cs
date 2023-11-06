using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.UnitOfWork;
using StatsSA.eRecruitment.IProvider.RequestAllApplications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.Provider.RequestAllApplications
{
    public class RequestAllApplicationsProvider : IRequestAllApplicationsProvider
    {
        private IUnitOfWork _unitOfWork;

        public RequestAllApplicationsProvider(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void AddRequestAllApplicationsManager(RequestAllApplication requestAllApplications)
        {
            _unitOfWork.RequestAllApplicationsManager.AddRequestAllApplicationsManager(requestAllApplications);
            _unitOfWork.SaveChanges();
        }

        public RequestAllApplication GetRequestAllApplicationsManager(int vacancyId)
        {
            return _unitOfWork.RequestAllApplicationsManager.GetRequestAllApplicationsManager(vacancyId);
        }

        public RequestAllApplication UpdateRequestAllApplicationsManager(RequestAllApplication requestAllApplications)
        {
            return _unitOfWork.RequestAllApplicationsManager.UpdateRequestAllApplicationsManager(requestAllApplications);
        }
    }
}
