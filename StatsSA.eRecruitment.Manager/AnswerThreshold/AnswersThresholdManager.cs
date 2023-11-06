using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.AnswerThreshold;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.Manager.AnswerThreshold
{
    public class AnswersThresholdManager : IAnswersThresholdManager
    {
        private eRecruitmentEntities _context;

        public AnswersThresholdManager(eRecruitmentEntities context)
        {
            _context = context;
        }

        public void AddThreshold(AnswersThreshold answersThreshold)
        {
            _context.AnswersThresholds.Add(answersThreshold);
        }

        public AnswersThreshold GetAnswersThreshold(int vacancyId)
        {
            return _context.AnswersThresholds.Where(x => x.VacancyId == vacancyId).FirstOrDefault();
        }

        public AnswersThreshold UpdateAnswersThreshold(AnswersThreshold answersThreshold)
        {
            _context.AnswersThresholds.Attach(answersThreshold);
            _context.Entry(answersThreshold).State = System.Data.Entity.EntityState.Modified;
            return answersThreshold;
        }
    }
}
