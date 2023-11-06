using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IManager.ViewApplicantQuestionAnswers
{
    public interface IViewApplicantQuestionAnswersManager
    {
        List<viewApplicantQuestionAnswer> GetAllApplicantQuestionAnswers(int applicationId, int vacancyId);
    }
}
