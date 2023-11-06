using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IManager.ManagePassword
{
    public interface IManagePasswordManager
    {
        void LockUnlockPassword(int userId, string tempPassword, bool isActive);
    }
}
