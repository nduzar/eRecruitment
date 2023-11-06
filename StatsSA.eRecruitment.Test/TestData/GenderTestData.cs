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
        public static class GenderTestData
        {
            public static Gender Male;
            public static Gender Female;

            public static void Create()
            {
                Male = UnitOfWork.GenderManager.AddGender(new GenderBuilder().Build());
                Female = UnitOfWork.GenderManager.AddGender(new GenderBuilder().WithGenderDesc("Female").Build());
                UnitOfWork.SaveChanges();
            }
        }
    }
}
