using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.AnswersGenericQuestions;
using StatsSA.eRecruitment.Manager.GenericRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.Manager.AnswersGenericQuestions
{
    public class AnswersGenericQuestionsManager : GenericRepository<AnswersGenericQuestion>,  IAnswersGenericQuestionsManager
    {
        private eRecruitmentEntities _context;

        public AnswersGenericQuestionsManager(eRecruitmentEntities context) : base(context)
        {
            _context = context;
        }

        public void AddAnswersGenericQuestion(List<AnswersGenericQuestion> answersGenericQuestion)
        {
            foreach (var item in answersGenericQuestion)
            {
                _context.AnswersGenericQuestions.Add(item);
            }
        }

        public void AddAnswerGenericQuestion(AnswersGenericQuestion answersGenericQuestion)
        {
            _context.AnswersGenericQuestions.Add(answersGenericQuestion);
        }

        public void DeleteAnswersGenericQuestion(AnswersGenericQuestion answerGenericQuestion)
        {
            base.Delete( x => x.Id == answerGenericQuestion.Id);
            //_context.AnswersGenericQuestions.Remove(answerGenericQuestion);
        }

        public void DeleteGenericQuestion(GenericQuestion genericQuestion)
        {
            base.Delete(x => x.QuestionId == genericQuestion.Id);
            //_context.AnswersGenericQuestions.Remove(answerGenericQuestion);
        }

        public IEnumerable<AnswersGenericQuestion> GetAllAnswersGenericQuestion(int questionId)
        {
            return _context.AnswersGenericQuestions.Where(x => x.QuestionId == questionId).ToList();
        }

        public IEnumerable<AnswersGenericQuestion> GetAllAnswersGenericQuestionByVacancy(int vacancyId)
        {
            return _context.AnswersGenericQuestions.Where(x => x.VacancyId == vacancyId).ToList();
        }

        public AnswersGenericQuestion GetAnswersGenericQuestionById(int id)
        {
            return _context.AnswersGenericQuestions.Where(x => x.Id == id).FirstOrDefault();
        }

        public void UpdateAnswersGenericQuestion(AnswersGenericQuestion answerGenericQuestion)
        {
            _context.AnswersGenericQuestions.Attach(answerGenericQuestion);
            _context.Entry(answerGenericQuestion).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
