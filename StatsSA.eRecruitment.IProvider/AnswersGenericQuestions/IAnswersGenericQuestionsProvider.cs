using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;

namespace StatsSA.eRecruitment.IProvider.AnswersGenericQuestions
{
    public interface IAnswersGenericQuestionsProvider
    {
        void AddAnswersGenericQuestion(List<AnswersGenericQuestion> answersGenericQuestion);

        IEnumerable<AnswersGenericQuestion> GetAllAnswersGenericQuestion(int questionId);

        AnswersGenericQuestion GetAnswersGenericQuestionById(int id);

        IEnumerable<AnswersGenericQuestion> GetAllAnswersGenericQuestionByVacancy(int vacancyId);

        void UpdateAnswersGenericQuestionById(List<AnswersGenericQuestion> answersGenericQuestion);

        void DeleteAnswersGenericQuestionById(AnswersGenericQuestion answersGenericQuestion);

        void DeleteGenericQuestion(GenericQuestion genericQuestion);
    }
}
