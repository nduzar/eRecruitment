using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.AnswersSpecificQuestions;
using StatsSA.eRecruitment.Manager.GenericRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.Manager.AnswersSpecificQuestions
{
    public class AnswersSpecificQuestionsManager : GenericRepository<AnswersSpecificQuestion>, IAnswersSpecificQuestionsManager
    {
        private eRecruitmentEntities _context;

        public AnswersSpecificQuestionsManager(eRecruitmentEntities context) : base(context)
        {
            _context = context;
        }

        public void AddAnswersSpecificQuestion(List<AnswersSpecificQuestion> answersSpecificQuestion)
        {
            foreach (var item in answersSpecificQuestion)
            {
                _context.AnswersSpecificQuestions.Add(item);
            }
        }

        public void DeleteAnswersSpecificQuestion(AnswersSpecificQuestion answersSpecificQuestion)
        {
            base.Delete(x => x.Id == answersSpecificQuestion.Id);
        }

        public void DeleteSpecificQuestion(SpecificQuestion specificQuestion)
        {
            base.Delete(x => x.QuestionId == specificQuestion.Id);

            var item = _context.SpecificQuestions.Where(x => x.Id == specificQuestion.Id).FirstOrDefault();
            _context.SpecificQuestions.Remove(item);

        }

        public IEnumerable<AnswersSpecificQuestion> GetAllAnswersSpecificQuestion(int questionId)
        {
            return _context.AnswersSpecificQuestions.Where(x => x.QuestionId == questionId).ToList();
        }

        public AnswersSpecificQuestion GetAnswersSpecificQuestionById(int id)
        {
            return _context.AnswersSpecificQuestions.Where(x => x.Id == id).FirstOrDefault();
        }

        public void UpdateAnswersSpecificQuestion(AnswersSpecificQuestion answerSpecificQuestion)
        {
            _context.AnswersSpecificQuestions.Attach(answerSpecificQuestion);
            _context.Entry(answerSpecificQuestion).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
