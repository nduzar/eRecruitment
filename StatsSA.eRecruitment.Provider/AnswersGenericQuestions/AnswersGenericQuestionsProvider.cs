using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.IManager.UnitOfWork;
using StatsSA.eRecruitment.IProvider.AnswersGenericQuestions;

namespace StatsSA.eRecruitment.Provider.AnswersGenericQuestionsProvider
{
    public class AnswersGenericQuestionsProvider : IAnswersGenericQuestionsProvider
    {
        private IUnitOfWork _unitOfWork;

        public AnswersGenericQuestionsProvider(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddAnswersGenericQuestion(List<AnswersGenericQuestion> answersGenericQuestion)
        {
            _unitOfWork.AnswersGenericQuestionsManager.AddAnswersGenericQuestion(answersGenericQuestion);
            _unitOfWork.SaveChanges();
        }

        public IEnumerable<AnswersGenericQuestion> GetAllAnswersGenericQuestion(int questionId)
        {
            return _unitOfWork.AnswersGenericQuestionsManager.GetAllAnswersGenericQuestion(questionId);
        }

        public AnswersGenericQuestion GetAnswersGenericQuestionById(int id)
        {
            return _unitOfWork.AnswersGenericQuestionsManager.GetAnswersGenericQuestionById(id);
        }

        public IEnumerable<AnswersGenericQuestion> GetAllAnswersGenericQuestionByVacancy(int vacancyId)
        {
            return _unitOfWork.AnswersGenericQuestionsManager.GetAllAnswersGenericQuestionByVacancy(vacancyId);
        }

        /// <summary>
        /// This method is actually a delete and add method 
        /// </summary>
        /// <param name="answersGenericQuestion"></param>
        public void UpdateAnswersGenericQuestionById(List<AnswersGenericQuestion> answersGenericQuestion)
        {
            foreach (var item in answersGenericQuestion)
            {
                var itemToBeUpdated = _unitOfWork.AnswersGenericQuestionsManager.GetAnswersGenericQuestionById(item.Id);

                if(itemToBeUpdated == null)
                {
                    itemToBeUpdated = new AnswersGenericQuestion();
                    itemToBeUpdated.QuestionId = item.QuestionId;
                    itemToBeUpdated.ShowSpecifyTextbox = item.ShowSpecifyTextbox;
                    itemToBeUpdated.VacancyId = item.VacancyId;
                    itemToBeUpdated.Weight = item.Weight;
                    itemToBeUpdated.Answer = item.Answer;
                    itemToBeUpdated.AutoReject = item.AutoReject;

                    List<AnswersGenericQuestion> list = new List<AnswersGenericQuestion>();
                    list.Add(itemToBeUpdated);
                    _unitOfWork.AnswersGenericQuestionsManager.AddAnswersGenericQuestion(list);
                }
                else
                {
                    itemToBeUpdated.QuestionId = item.QuestionId;
                    itemToBeUpdated.ShowSpecifyTextbox = item.ShowSpecifyTextbox;
                    itemToBeUpdated.VacancyId = item.VacancyId;
                    itemToBeUpdated.Weight = item.Weight;
                    itemToBeUpdated.Answer = item.Answer;
                    itemToBeUpdated.AutoReject = item.AutoReject;

                    _unitOfWork.AnswersGenericQuestionsManager.UpdateAnswersGenericQuestion(itemToBeUpdated);                    
                }
                _unitOfWork.SaveChanges();
            }            
        }

        public void DeleteAnswersGenericQuestionById(AnswersGenericQuestion answersGenericQuestion)
        {
            _unitOfWork.AnswersGenericQuestionsManager.DeleteAnswersGenericQuestion(answersGenericQuestion);
            _unitOfWork.SaveChanges();
        }

        public void DeleteGenericQuestion(GenericQuestion genericQuestion)
        {
            _unitOfWork.AnswersGenericQuestionsManager.DeleteGenericQuestion(genericQuestion);
            _unitOfWork.SaveChanges();
        }        
    }
}
