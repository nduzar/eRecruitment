using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;

namespace StatsSA.eRecruitment.IManager.RequestAllApplications
{
    public interface IRequestAllApplicationsManager
    {
        void AddRequestAllApplicationsManager(RequestAllApplication requestAllApplications);

        RequestAllApplication GetRequestAllApplicationsManager(int vacancyId);

        RequestAllApplication UpdateRequestAllApplicationsManager(RequestAllApplication requestAllApplications);
    }
}
