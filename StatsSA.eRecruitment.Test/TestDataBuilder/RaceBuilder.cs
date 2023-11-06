using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.Test.TestDataBuilder
{
    public class RaceBuilder
    {
        private string RaceDesc = "African";
        private string MaintUser = "TestUser";
        private DateTime MaintDate = DateTime.Now;

        public RaceBuilder WithRaceDesc(string raceDesc)
        {
            RaceDesc = raceDesc;
            return this;
        }
        public Race Build()
        {
            return new Race
            {
                RaceDesc = this.RaceDesc,
                MaintUser = this.MaintUser,
                MaintDate = this.MaintDate
            };
        }
    }
}
