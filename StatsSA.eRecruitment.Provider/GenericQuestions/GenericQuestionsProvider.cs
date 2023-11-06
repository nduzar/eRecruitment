using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.UnitOfWork;
using StatsSA.eRecruitment.IProvider.GenericQuestions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.Provider.GenericQuestions
{
    public class GenericQuestionsProvider : IGenericQuestionProvider
    {

        private IUnitOfWork _unitOfWork;

        public GenericQuestionsProvider(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<GenericQuestion> GetAllQuestions()
        {
            return _unitOfWork.GenericQuestionManager.GetAllQuestions();
        }

        public void DeleteGenericQuestion(GenericQuestion question)
        {
            _unitOfWork.GenericQuestionManager.DeleteGenericQuestion(question);
            _unitOfWork.SaveChanges();
        }

        public void SaveAllQuestions(List<GenericQuestion> genericQuestions)
        {
            _unitOfWork.GenericQuestionManager.SaveAllQuestions(genericQuestions);
            _unitOfWork.SaveChanges();
        }
    }
}
