using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IManager.Programmes
{
    public interface IProgrammeManager
    {        
       IEnumerable<Programme> GetAllProgrammes();

       Programme GetProgrammeById(int programmeId);
    }
}
