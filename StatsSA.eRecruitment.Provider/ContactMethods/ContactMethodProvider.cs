using StatsSA.eRecruitment.IProvider.ContactMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.UnitOfWork;

namespace StatsSA.eRecruitment.Provider.ContactMethods
{
    public class ContactMethodProvider : IContactMethodProvider
    {
        private IUnitOfWork _unitOfWork;
        public ContactMethodProvider(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<ContactMethod> GetContactMethods()
        {
            return _unitOfWork.ContactMethodManager.GetContactMethods();
        }

        public ContactMethod GetContactMethodById(int contactMethodId)
        {
            return _unitOfWork.ContactMethodManager.GetContactMethodById(contactMethodId);
        }
    }
}
