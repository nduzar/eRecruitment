using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IManager.ContactMethods
{
    public interface IContactMethodManager
    {
        IEnumerable<ContactMethod> GetContactMethods();
        ContactMethod GetContactMethodById(int contactMethodId);
    }
}
