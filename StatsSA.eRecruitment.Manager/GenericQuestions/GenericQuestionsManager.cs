using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.IGenericQuestions;
using StatsSA.eRecruitment.Manager.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.Manager.GenericQuestions
{
    public class GenericQuestionsManager : GenericRepository<GenericQuestion>, IGenericQuestionsManager
    {
        private eRecruitmentEntities _context;

        public GenericQuestionsManager(eRecruitmentEntities context) : base(context)
        {
            _context = context;
        }
        public IEnumerable<GenericQuestion> GetAllQuestions()
        {
            return base.GetAll().ToList();
        }

        public void SaveAllQuestions(List<GenericQuestion> genericQuestions)
        {
            foreach (var item in genericQuestions)
            {
                item.Description = item.Question;

                if(Exists(item))
                {
                    base.Update(item);
                }
                else
                {
                    base.Insert(item);
                }
            }
        }

        public void DeleteGenericQuestion(GenericQuestion question)
        {
            base.Delete(question);
        }

        private bool Exists(GenericQuestion genericQuestion)
        {
            return _context.GenericQuestions.Where(x => x.Id == genericQuestion.Id).Any();
        }
    }
}
