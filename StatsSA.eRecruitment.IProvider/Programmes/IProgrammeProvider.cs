using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IProvider.Programmes
{
    public interface IProgrammeProvider
    {
        IEnumerable<Programme> GetAllProgrammes();

        Programme GetProgrammeById(int programmeId);
    }
}
