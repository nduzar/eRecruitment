using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.UnitOfWork;
using StatsSA.eRecruitment.IProvider.SpecificQuestions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.Provider.SpecificQuestions
{
    public class SpecificQuestionsProvider : ISpecificQuestionsProvider
    {
        private IUnitOfWork _unitOfWork;

        public SpecificQuestionsProvider(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public SpecificQuestion AddSpecificQuestions(SpecificQuestion specificQuestions)
        {
            return _unitOfWork.SpecificQuestionManager.AddSpecificQuestions(specificQuestions);
        }

        public IEnumerable<SpecificQuestion> GetAllQuestions(int vacancyId)
        {
            return _unitOfWork.SpecificQuestionManager.GetAllQuestions(vacancyId);
        }

        public SpecificQuestion GetSpecificQuestionById(int id)
        {
            return _unitOfWork.SpecificQuestionManager.GetSpecificQuestionById(id);
        }

        public SpecificQuestion UpdateSpecificQuestionById(SpecificQuestion specificQuestion)
        {
            var itemToBeEditted = _unitOfWork.SpecificQuestionManager.GetSpecificQuestionById(specificQuestion.Id);

            if(itemToBeEditted == null)
            {
                itemToBeEditted = new SpecificQuestion();
                itemToBeEditted.Question = specificQuestion.Question;
                itemToBeEditted.VacancyId = specificQuestion.VacancyId;
                _unitOfWork.SpecificQuestionManager.AddSpecificQuestions(itemToBeEditted);
                
            } 
            else
            {
                itemToBeEditted.Question = specificQuestion.Question;
                itemToBeEditted.VacancyId = specificQuestion.VacancyId;
                _unitOfWork.SpecificQuestionManager.UpdateSpecificQuestionById(itemToBeEditted);
               
            }
            _unitOfWork.SaveChanges();

            return specificQuestion;
        }
    }
}
