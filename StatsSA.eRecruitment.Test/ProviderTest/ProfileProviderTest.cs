using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StatsSA.eRecruitment.IManager;
using StatsSA.eRecruitment.Manager.UnitOfWork;
using StatsSA.eRecruitment.Provider.ApplicantProfiles;
using StatsSA.eRecruitment.Entities;
using System.Collections;
using System.Collections.Generic;

namespace StatsSA.eRecruitment.Test.ProviderTest
{
    [TestClass]
    public class ProfileProviderTest
    {
        private UnitOfWork _unitOfWork;
        ApplicantProfileProvider applicantProvider;
        [TestInitialize]
        public void TestInitialize()
        {
            _unitOfWork = new UnitOfWork();
            applicantProvider = new ApplicantProfileProvider(_unitOfWork);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            // _unitOfWork.RollbackTransaction();
        }
        [TestMethod]
        public void getApplicantProfile()
        {
            var prof = applicantProvider.GetApplicantProfileByUserId(15);
            Assert.IsNotNull(prof);
        }

        
        [TestMethod]
        public void saveApplicantProfile()
        {
            var prof = new ApplicantProfile
            {
                UserId = 146,
                FirstNames = "FN",
                Surname = "LN",
                DateOfBirth = DateTime.Now,
                IdentityNumber = "998551545621",
                RaceId = 86,
                GenderId = 41,
                Citizen = true,
                Nationality = null,
                WorkPermit = false,
                PreviousDismissal = false,
                OccupationRegistrationDate = null,
                OccupationRegistrationDesc = null,
                MaintUser = "H@z",
                MaintDate = DateTime.Now
            };
            var result = _unitOfWork.ApplicantProfileManager.AddApplicantProfile(prof);
            Assert.IsNotNull(result);
        }
       
    }
}
