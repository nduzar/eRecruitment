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
        public static class UserTestData
        {
            public static User SuperUser;
            public static User HennieJ;
            public static User ThobaniZ;
            public static void Create()
            {
                SuperUser = UnitOfWork.UserManager.AddUser(new UserBuilder().WithCellphone("0727896564").WithEmail("dumisanek@treasury.gov.za").Build());
                HennieJ = UnitOfWork.UserManager.AddUser(new UserBuilder().WithUserName("HennieJ").WithPassword("P@ssw0rd").WithCellphone("0768121992").WithEmail("henniej@treasury.gov.za").Build());
                ThobaniZ = UnitOfWork.UserManager.AddUser(new UserBuilder().WithUserName("ThobaniZ").WithPassword("march2017").Build());
                UnitOfWork.SaveChanges();
            }
        }
    }
}
