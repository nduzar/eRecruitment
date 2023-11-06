using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.UnitOfWork;
using StatsSA.eRecruitment.IProvider.Programmes;

namespace StatsSA.eRecruitment.Provider.Programmes
{
    public class ProgrammeProvider : IProgrammeProvider
    {
        private IUnitOfWork _unitOfWork;
        public ProgrammeProvider(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Programme> GetAllProgrammes()
        {
            return _unitOfWork.ProgrammeManager.GetAllProgrammes();
        }

        public Programme GetProgrammeById(int programmeId)
        {
            return _unitOfWork.ProgrammeManager.GetProgrammeById(programmeId);
        }
    }
}
