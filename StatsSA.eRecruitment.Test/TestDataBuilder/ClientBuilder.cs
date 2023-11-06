using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.Test.TestDataBuilder
{
    public class ClientBuilder
    {
        private string ClientId = "ngAuthApp";
        private string Secret = "StatisticsSA@BM";
        private string Name = "AngularJS front-end Application";
        private int ApplicationType = 0;
        private bool Active = true;
        private int RefreshTokenLifeTime = 7200;
        private string AllowedOrigin = "http://localhost:10762";
        private string CreatedBy = "TestUser";
        private DateTime CreateDate = DateTime.Now;
        private string ModifiedBy = "TestUser";
        private DateTime ModifiedDate = DateTime.Now;

        public Client Build()
        {
            return new Client
            {
                ClientId = ClientId,
                Secret = Helper.GetHash(Secret),
                Name = Name,
                ApplicationType = ApplicationType,
                Active = Active,
                RefreshTokenLifeTime = RefreshTokenLifeTime,
                AllowedOrigin = AllowedOrigin,
                CreateDate = CreateDate,
                CreatedBy = CreatedBy,
                ModifiedBy = ModifiedBy,
                ModifiedDate = ModifiedDate
            };
        }
    }
}
