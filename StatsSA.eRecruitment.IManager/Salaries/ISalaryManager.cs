using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;

namespace StatsSA.eRecruitment.IManager.Salaries
{
   public interface ISalaryManager
    {
        IEnumerable<Salary> GetLevels();

        void updateSalary(Salary salary);
        Salary getSalarybyId(int salaryId);

    }
}
