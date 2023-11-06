﻿using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IManager.Roles
{
    public interface IRoleManager
    {
        Role AddRole(Role role);
        IEnumerable<Role> GetRoles();
        IEnumerable<Role> RolesByClientId(string clientId);
    }
}
