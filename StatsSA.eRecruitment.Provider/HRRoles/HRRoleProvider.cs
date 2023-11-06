using StatsSA.eRecruitment.IProvider.HRRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.UnitOfWork;

namespace StatsSA.eRecruitment.Provider.HRRoles
{
    public class HRRoleProvider : IHRRoleProvider
    {
        private IUnitOfWork _unitOfWork;
        public HRRoleProvider(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<HRRole> GetAllHRRoles()
        {
            return this._unitOfWork.HRRoleManager.GetAllHRRoles();
        }

        public HRRole GetRolebyId(int hrRoleId)
        {
            return this._unitOfWork.HRRoleManager.GetRolebyId(hrRoleId);
        }
        
    }
}
