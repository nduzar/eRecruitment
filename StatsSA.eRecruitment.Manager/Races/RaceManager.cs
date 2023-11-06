using StatsSA.eRecruitment.IManager.Races;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.Manager.GenericRepository;

namespace StatsSA.eRecruitment.Manager.Races
{
    public class RaceManager : IRaceManager
    {
        private eRecruitmentEntities _context;
        public RaceManager(eRecruitmentEntities context)
        {
            _context = context;
        }
        public Race AddRace(Race race)
        {
            return _context.Races.Add(race);
        }

        public IEnumerable<Race> GetAllRaces()
        {
            return _context.Races.ToList();
        }

        public Race GetRaceById(int raceId)
        {
            return _context.Races.FirstOrDefault(race => race.RaceId == raceId);
        }
    }
}
