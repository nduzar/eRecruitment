using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;
using StatsSA.eRecruitment.Manager.UnitOfWork;
using StatsSA.eRecruitment.Test.TestDataBuilder;

namespace StatsSA.eRecruitment.Test.DataAccessTests
{
    [TestClass]
    public class VacancyManagerTest
    {
        private UnitOfWork _unitOfWork;
        [TestInitialize]
        public void TestInitialize()
        {
            var objectmother = ObjectMother.Instance;

            _unitOfWork = new UnitOfWork();
            _unitOfWork.BeginTransaction();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _unitOfWork.RollbackTransaction();
            _unitOfWork.Dispose();
        }

        [TestMethod]
        public void Add_vacancy_should_return_new_vacancy_with_vacancyId()
        {
            //Arrange
            var vacancy = new VacancyBuilder().build();
            bool expectedResult = true;

            //Act
            var result = _unitOfWork.VacancyManager.AddVacancy(vacancy);
            _unitOfWork.SaveChanges();

            //Assert
            Assert.AreEqual(expectedResult, (result.VacancyId > 0));
        }
    }
}
