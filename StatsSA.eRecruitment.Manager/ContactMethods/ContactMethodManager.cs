using StatsSA.eRecruitment.IManager.ContactMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.Manager.GenericRepository;

namespace StatsSA.eRecruitment.Manager.ContactMethods
{
    public class ContactMethod : IContactMethodManager
    {
        private eRecruitmentEntities _context;
        public ContactMethod(eRecruitmentEntities context)
        {
            _context = context;
        }
        public IEnumerable<Entities.ContactMethod> GetContactMethods()
        {
            return _context.ContactMethods.ToList();
        }

        public Entities.ContactMethod GetContactMethodById(int contactMethodId)
        {
            return _context.ContactMethods.FirstOrDefault(contMeth => contMeth.ContactMethodId == contactMethodId);
        }
    }
}
