using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.Entities.Enums
{
    public enum ApplicationStatuses
    {
        Received = 1,
        InProgress = 2,
        Shortlisted = 3,    
        Successful = 4,
        Rejected = 5,
        AutoRejected = 6,
        DidNotMeetRequirements = 7,
        MetRequirements = 9,
        HRShortlisted = 10,
        HiringManagerShortlisted = 11,
        HRRejected = 12,
        HiringManagerRejected = 13,
        ScheduledInterview = 14,
        Unsuccessful = 15,

    }
}
