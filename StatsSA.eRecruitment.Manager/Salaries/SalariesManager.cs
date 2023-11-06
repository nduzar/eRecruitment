using System;
using StatsSA.eRecruitment.IManager.Salaries;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.Manager.GenericRepository;

namespace StatsSA.eRecruitment.Manager.Salaries
{
   public class SalaryManager : GenericRepository<Salary>, ISalaryManager
    {
        private eRecruitmentEntities _context;

        public SalaryManager(eRecruitmentEntities context) :base(context)
        {
            _context = context;
        }
        public IEnumerable<Salary> GetLevels()
        {
            return base.GetAll();
        }
        public void updateSalary(Salary salary)
        {
            base.Update(salary);
        }
        public Salary getSalarybyId(int salaryId)
        {
            return base.Get(sal => sal.SalaryId == salaryId);
        }
    }
}
