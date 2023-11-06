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
        public static class ClientTestData
        {
            public static Client ngAuthApp;

            public static void Create()
            {
                ngAuthApp = UnitOfWork.ClientManager.AddClient(new ClientBuilder().Build());
                UnitOfWork.SaveChanges();
            }
        }
    }
}
