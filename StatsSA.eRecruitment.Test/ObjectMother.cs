using StatsSA.eRecruitment.Manager.UnitOfWork;
using StatsSA.eRecruitment.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.Test
{
    public partial class ObjectMother
    {
        private static readonly ObjectMother instance = new ObjectMother();
        public static UnitOfWork UnitOfWork;
        static ObjectMother()
        {
            using(var context = new eRecruitmentTestEntities())
            {
                context.PrepareDataBaseForTesting();
                context.SaveChanges();
            }
            UnitOfWork = new UnitOfWork();
            UnitOfWork.BeginTransaction();

            //Lookups
            RaceTestData.Create();
            GenderTestData.Create();
            ClientTestData.Create();

            //other            
            VacancyTestData.Create();
            UserTestData.Create();
            ApplicantProfileTestData.Create();
            //ApplicationTestData.Create();


            UnitOfWork.CommitTransaction();
        }
        private ObjectMother()
        {
            
        }
        public static ObjectMother Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
