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
        public static class ApplicantProfileTestData
        {
            private static ApplicantProfile SuperUserProfile;
            public static ApplicantProfile HenniesProfile;
            public static void Create()
            {
                var superUser = UserTestData.SuperUser;
                SuperUserProfile = UnitOfWork.ApplicantProfileManager.AddApplicantProfile(new ApplicantProfileBuilder().Build(superUser));
                UnitOfWork.SaveChanges();

                var henniesUser = UserTestData.HennieJ;
                HenniesProfile = UnitOfWork.ApplicantProfileManager.AddApplicantProfile(
                    new ApplicantProfileBuilder()
                    .WithSurname("Jansen van Nieuwenhuizen")
                    .WithFirstNames("Hennie")
                    .WithDateOfBirth(Convert.ToDateTime("1981/01/01"))
                    .WithIdentityNumber("8101011235426")
                    .WithRace(ObjectMother.RaceTestData.White)
                    .WithGender(ObjectMother.GenderTestData.Male)
                    .WithDisability(false)
                    .WithCitizen(true)
                    .WithWorkPermit(false)
                    .WithPreviousDismissal(false)
                    .WithCriminalOffence(false)
                    .WithOccupationRegistrationDate(Convert.ToDateTime("2010/01/01"))
                    .WithOccupationRegistrationDesc("Some registration description")
                    .Build(henniesUser));
                UnitOfWork.SaveChanges();
            }
        }
    }
}
