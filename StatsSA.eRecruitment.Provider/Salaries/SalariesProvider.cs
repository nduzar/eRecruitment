using System;
using System.Collections.Generic;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.UnitOfWork;
using StatsSA.eRecruitment.IProvider.Salaries;

namespace StatsSA.eRecruitment.Provider.Salaries
{
    public class SalariesProvider : ISalariesProvider
    {
        private IUnitOfWork _unitOfWork;
        public SalariesProvider(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public IEnumerable<Salary> GetAllSalaries()
        {
            return _unitOfWork.SalaryManager.GetLevels();
        }

        IEnumerable<Salary> ISalariesProvider.GetAllSalaries()
        {
            return _unitOfWork.SalaryManager.GetLevels();
        }

        public void updateSalary(Salary salary)
        {
            //var oldSalary = this.getSalarybyId(salary.SalaryId);
            //oldSalary.SalaryNotch = salary.SalaryNotch;
            //oldSalary.MaintDate = DateTime.Now;
            _unitOfWork.BeginTransaction();
            _unitOfWork.SalaryManager.updateSalary(salary);
            _unitOfWork.SaveChanges();
            _unitOfWork.CommitTransaction();

            ;
        }

        public Salary getSalarybyId(int salaryId)
        {
            var salary = _unitOfWork.SalaryManager.getSalarybyId(salaryId);
            return salary;

        }
    }
}
