using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.Test.TestDataBuilder
{
    public class GenderBuilder
    {
        private string GenderDesc = "Male";
        private string MaintUser = "TestUser";
        private DateTime MaintDate = DateTime.Now;

        public GenderBuilder WithGenderDesc(string genderDesc)
        {
            GenderDesc = genderDesc;
            return this;
        }
        public Gender Build()
        {
            return new Gender
            {
                GenderDesc = this.GenderDesc,
                MaintUser = this.MaintUser,
                MaintDate = this.MaintDate
            };
        }
    }
}
