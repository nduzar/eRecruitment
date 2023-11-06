using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.IManager.UnitOfWork;
using StatsSA.eRecruitment.IProvider.AnswersSpecificQuestions;

namespace StatsSA.eRecruitment.Provider.AnswersSpecificQuestions
{
    public class AnswersSpecificQuestionsProvider : IAnswersSpecificQuestionsProvider
    {
        private IUnitOfWork _unitOfWork;

        public AnswersSpecificQuestionsProvider(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddAnswersSpecificQuestion(List<AnswersSpecificQuestion> answersGenericQuestion)
        {
            _unitOfWork.AnswersSpecificQuestionsManager.AddAnswersSpecificQuestion(answersGenericQuestion);
            _unitOfWork.SaveChanges();
        }

        public void DeleteAnswersSpecificQuestionById(AnswersSpecificQuestion answersSpecificQuestion)
        {
            _unitOfWork.AnswersSpecificQuestionsManager.DeleteAnswersSpecificQuestion(answersSpecificQuestion);
            _unitOfWork.SaveChanges();
        }

        public void DeleteSpecificQuestion(SpecificQuestion specificQuestion)
        {
            _unitOfWork.AnswersSpecificQuestionsManager.DeleteSpecificQuestion(specificQuestion);
            _unitOfWork.SaveChanges();
        }        

        public IEnumerable<AnswersSpecificQuestion> GetAllAnswersSpecificQuestion(int questionId)
        {
            return _unitOfWork.AnswersSpecificQuestionsManager.GetAllAnswersSpecificQuestion(questionId);
        }

        public AnswersSpecificQuestion GetAnswersSpecificQuestionById(int id)
        {
            return _unitOfWork.AnswersSpecificQuestionsManager.GetAnswersSpecificQuestionById(id);
        }

        public AnswersSpecificQuestion UpdateAnswersSpecificQuestionById(AnswersSpecificQuestion answersSpecificQuestion)
        {
            var itemToBeUpdated = _unitOfWork.AnswersSpecificQuestionsManager.GetAnswersSpecificQuestionById(answersSpecificQuestion.Id);
            itemToBeUpdated.Answer = answersSpecificQuestion.Answer;
            itemToBeUpdated.AutoReject = answersSpecificQuestion.AutoReject;
            itemToBeUpdated.QuestionId = answersSpecificQuestion.QuestionId;
            itemToBeUpdated.Weight = answersSpecificQuestion.Weight;

            _unitOfWork.AnswersSpecificQuestionsManager.UpdateAnswersSpecificQuestion(itemToBeUpdated);
            _unitOfWork.SaveChanges();
            return itemToBeUpdated;
        }
    }
}
