﻿using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IManager.Requirements
{
    public interface IRequirementManager
    {
        Requirement GetRequirementById(int ApplicationId);
        IEnumerable<Requirement> GetAllRequirements();
    }
}
