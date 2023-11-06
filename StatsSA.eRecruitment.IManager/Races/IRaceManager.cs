using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IManager.Races
{
    public interface IRaceManager
    {
        Race AddRace(Race race);

        IEnumerable<Race> GetAllRaces();
        Race GetRaceById(int raceId);
    }
}
