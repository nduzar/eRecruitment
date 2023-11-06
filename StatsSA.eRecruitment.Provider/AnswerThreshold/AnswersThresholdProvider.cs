using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.UnitOfWork;
using StatsSA.eRecruitment.IProvider.AnswerThreshold;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.Provider.AnswerThreshold
{
    class AnswersThresholdProvider : IAnswersThresholdProvider
    {
        private IUnitOfWork _unitOfWork;

        public AnswersThresholdProvider(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void AddThreshold(AnswersThreshold answersThreshold)
        {
            _unitOfWork.AnswersThresholdManager.AddThreshold(answersThreshold);
            _unitOfWork.SaveChanges();
        }

        public AnswersThreshold GetAnswersThreshold(int vacancyId)
        {
            return _unitOfWork.AnswersThresholdManager.GetAnswersThreshold(vacancyId);
        }

        public AnswersThreshold UpdateAnswersThreshold(AnswersThreshold answersThreshold)
        {
            return _unitOfWork.AnswersThresholdManager.UpdateAnswersThreshold(answersThreshold);
        }
    }
}
