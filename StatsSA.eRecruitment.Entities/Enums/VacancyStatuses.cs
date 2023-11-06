using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.Entities.Enums
{
    public enum VacancyStatuses
    {
        Captured = 1,
        ApproverRejected = 2,
        Approved = 3,
        AuthoriserRejected = 4,
        Authorised = 5,
        Screened = 6,
        Shortlisted = 7,
        Interviewed = 8,
        Filled = 9,
        Cancelled = 10
    }
}
