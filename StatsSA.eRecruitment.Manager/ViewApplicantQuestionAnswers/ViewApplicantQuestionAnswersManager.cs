using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.ViewApplicantQuestionAnswers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.Manager.ViewApplicantQuestionAnswers
{
    public class ViewApplicantQuestionAnswersManager : IViewApplicantQuestionAnswersManager
    {
        private readonly eRecruitmentEntities _context;

        public ViewApplicantQuestionAnswersManager(eRecruitmentEntities context)
        {
            _context = context;
        }
        public List<viewApplicantQuestionAnswer> GetAllApplicantQuestionAnswers(int applicationId, int vacancyId)
        {
            return _context.viewApplicantQuestionAnswers.Where(x => x.ApplicationId == applicationId && x.VacancyId == vacancyId).ToList();
        }
    }
}
