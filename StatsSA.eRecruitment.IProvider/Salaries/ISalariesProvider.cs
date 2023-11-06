using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IProvider.Salaries
{
    public interface ISalariesProvider
    {
        IEnumerable<Salary> GetAllSalaries();

        void updateSalary(Salary salary);

        Salary getSalarybyId(int salaryId);


    }
}
