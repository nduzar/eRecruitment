using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IProvider.ApplicationStatuses
{
    public interface IApplicationStatusProvider
    {
        ApplicationStatus GetApplicationStatus(int applicationStatusId);

    }
}
