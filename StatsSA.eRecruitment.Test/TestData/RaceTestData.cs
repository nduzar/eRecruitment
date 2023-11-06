using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.Test.TestDataBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.Test
{
    public partial class ObjectMother
    {
        public static class RaceTestData
        {
            public static Race African;
            public static Race White;
            public static Race Coloured;
            public static Race Indian;

            public static void Create()
            {
                African = UnitOfWork.RaceManager.AddRace(new RaceBuilder().Build());
                White = UnitOfWork.RaceManager.AddRace(new RaceBuilder().WithRaceDesc("White").Build());
                Indian = UnitOfWork.RaceManager.AddRace(new RaceBuilder().WithRaceDesc("Indian").Build());
                Coloured = UnitOfWork.RaceManager.AddRace(new RaceBuilder().WithRaceDesc("Coloured").Build());
                UnitOfWork.SaveChanges();
            }        

        }
    }
}
