using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.UnitOfWork;
using StatsSA.eRecruitment.IProvider.ViewApplicantQuestionAnswers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.Provider.ViewApplicantQuestionAnswers
{
    public class ViewApplicantQuestionAnswersProvider : IViewApplicantQuestionAnswersProvider
    {
        private IUnitOfWork _unitOfWork;

        public ViewApplicantQuestionAnswersProvider(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public List<viewApplicantQuestionAnswer> GetAllApplicantQuestionAnswers(int applicationId, int vacancyId)
        {
            return _unitOfWork.ViewApplicantQuestionAnswersManager.GetAllApplicantQuestionAnswers(applicationId, vacancyId);
        }
    }
}
