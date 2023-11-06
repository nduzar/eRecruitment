using System;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IProvider.ClientRoles;
using StatsSA.eRecruitment.IManager.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.Provider.ClientRoles
{
    public class ClientRoleProvider:IClientRoleProvider
    {
        private IUnitOfWork _unitOfWork;
        public ClientRoleProvider(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<ClientRole> SaveClientRole(IEnumerable<ClientRole>ClientRoleToAdd,IEnumerable<ClientRole>ClientRoleToRemove)
        {
            _unitOfWork.BeginTransaction();
            if (ClientRoleToAdd.Any())
            {
                _unitOfWork.ClientRoleManager.AddClientRole(ClientRoleToAdd);
            }
            if (ClientRoleToRemove.Any())
            {
                _unitOfWork.ClientRoleManager.RemoveClientRole(ClientRoleToRemove);
            }
            _unitOfWork.SaveChanges();
            _unitOfWork.CommitTransaction();
            return _unitOfWork.ClientRoleManager.GetClientRoles();
        }
    }
}
