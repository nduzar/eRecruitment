using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.SpecificQuestions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.Manager.SpecificQuestions
{
    public class SpecificQuestionsManager : ISpecificQuestionsManager
    {

        private eRecruitmentEntities _context;

        public SpecificQuestionsManager(eRecruitmentEntities context)
        {
            _context = context;
        }

        public SpecificQuestion AddSpecificQuestions(SpecificQuestion specifcQuestions)
        {
            var question = _context.SpecificQuestions.Add(specifcQuestions);
            _context.SaveChanges();
            return question;
        }

        public IEnumerable<SpecificQuestion> GetAllQuestions(int vacancyId)
        {
            return _context.SpecificQuestions.Where(x => x.VacancyId == vacancyId).ToList();
        }

        public SpecificQuestion GetSpecificQuestionById(int id)
        {
            return _context.SpecificQuestions.Where(x => x.Id == id).FirstOrDefault();
        }

        public SpecificQuestion UpdateSpecificQuestionById(SpecificQuestion specificQuestion)
        {
            _context.SpecificQuestions.Attach(specificQuestion);
            _context.Entry(specificQuestion).State = System.Data.Entity.EntityState.Modified;
            return specificQuestion;
        }
    }
}
