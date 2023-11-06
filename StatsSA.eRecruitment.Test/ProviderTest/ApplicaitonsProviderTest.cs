using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StatsSA.eRecruitment.IManager;
using StatsSA.eRecruitment.Manager.UnitOfWork;
using StatsSA.eRecruitment.Provider.Applications;
using StatsSA.eRecruitment.Entities;
using System.Collections;
using System.Collections.Generic;

namespace StatsSA.eRecruitment.Test.DataAccessTests
{
    [TestClass]
    public class ApplicaitonsTest
    {
        private UnitOfWork _unitOfWork;
        ApplicationProvider applicationProvider;
        [TestInitialize]
        public void TestInitialize()
        {
            _unitOfWork = new UnitOfWork();
            applicationProvider = new ApplicationProvider(_unitOfWork);
        }

        [TestCleanup]
        public void TestCleanup()
        {
           // _unitOfWork.RollbackTransaction();
        }
        [TestMethod]
        public void GetApplicationsPerUserID()
        {
            var applicationList = applicationProvider.GetApplicantApplication(15);
            Assert.IsNotNull(applicationList);


        }
    }
}
