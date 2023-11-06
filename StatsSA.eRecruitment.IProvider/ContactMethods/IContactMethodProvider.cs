using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IProvider.ContactMethods
{
    public interface IContactMethodProvider {

        IEnumerable<ContactMethod> GetContactMethods();
        ContactMethod GetContactMethodById(int contactMethodId);
    }
}
