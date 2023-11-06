using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IManager.AnswersGenericQuestions
{
    public interface IAnswersGenericQuestionsManager
    {
        void AddAnswersGenericQuestion(List<AnswersGenericQuestion> answersGenericQuestion);

        void AddAnswerGenericQuestion(AnswersGenericQuestion answersGenericQuestion);

        void UpdateAnswersGenericQuestion(AnswersGenericQuestion answerGenericQuestion);

        void DeleteAnswersGenericQuestion(AnswersGenericQuestion answerGenericQuestion);

        IEnumerable<AnswersGenericQuestion> GetAllAnswersGenericQuestion(int questionId);

        IEnumerable<AnswersGenericQuestion> GetAllAnswersGenericQuestionByVacancy(int vacancyId);

        AnswersGenericQuestion GetAnswersGenericQuestionById(int id);

        void DeleteGenericQuestion(GenericQuestion genericQuestion);
    }
}
