using StatsSA.eRecruitment.IProvider.Races;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.UnitOfWork;

namespace StatsSA.eRecruitment.Provider.Races
{
    public class RaceProvider : IRaceProvider
    {
        private IUnitOfWork _unitOfWork;
        public RaceProvider(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Race AddRace(Race race)
        {
            _unitOfWork.RaceManager.AddRace(race);
            _unitOfWork.SaveChanges();
            return race;
        }

        public IEnumerable<Race> GetAllRaces()
        {
            return _unitOfWork.RaceManager.GetAllRaces();
        }

        public Race GetRaceById(int raceId)
        {
            return _unitOfWork.RaceManager.GetRaceById(raceId);
        }
    }
}
