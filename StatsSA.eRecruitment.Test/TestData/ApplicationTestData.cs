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
        public static class ApplicationTestData
        {
            public static Application DirectorApplication;
            public static void Create()
            {
                DirectorApplication = new ApplicationBuilder().Build(ObjectMother.ApplicantProfileTestData.HenniesProfile, ObjectMother.VacancyTestData.Director);
                UnitOfWork.ApplicationManager.AddApplication(DirectorApplication);
                UnitOfWork.SaveChanges();
            }
        }
    }
}
