using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IProvider.RequestAllApplications
{
    public interface IRequestAllApplicationsProvider
    {
        void AddRequestAllApplicationsManager(RequestAllApplication requestAllApplications);

        RequestAllApplication GetRequestAllApplicationsManager(int vacancyId);

        RequestAllApplication UpdateRequestAllApplicationsManager(RequestAllApplication requestAllApplications);
    }
}
