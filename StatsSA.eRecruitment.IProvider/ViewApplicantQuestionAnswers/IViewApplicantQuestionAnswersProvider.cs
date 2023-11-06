using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IProvider.ViewApplicantQuestionAnswers
{
    public interface IViewApplicantQuestionAnswersProvider
    {
        List<viewApplicantQuestionAnswer> GetAllApplicantQuestionAnswers(int applicationId, int vacancyId);
    }
}
