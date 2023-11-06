using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.RequestAllApplications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.Manager.RequestAllApplications
{
    public class RequestAllApplicationsManager : IRequestAllApplicationsManager
    {
        private eRecruitmentEntities _context;
        public RequestAllApplicationsManager(eRecruitmentEntities context)
        {
            _context = context;
        }
        public void AddRequestAllApplicationsManager(RequestAllApplication requestAllApplications)
        {
            _context.RequestAllApplications.Add(requestAllApplications);
        }

        public RequestAllApplication GetRequestAllApplicationsManager(int vacancyId)
        {
            return _context.RequestAllApplications.Where(x => x.VacancyId == vacancyId).FirstOrDefault();
        }

        public RequestAllApplication UpdateRequestAllApplicationsManager(RequestAllApplication requestAllApplications)
        {
            _context.RequestAllApplications.Attach(requestAllApplications);
            _context.Entry(requestAllApplications).State = System.Data.Entity.EntityState.Modified;
            return requestAllApplications;
        }
    }
}
