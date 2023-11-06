using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;

namespace StatsSA.eRecruitment.IProvider.AnswersSpecificQuestions
{
    public interface IAnswersSpecificQuestionsProvider
    {
        void AddAnswersSpecificQuestion(List<AnswersSpecificQuestion> answersSpecificQuestion);

        IEnumerable<AnswersSpecificQuestion> GetAllAnswersSpecificQuestion(int questionId);

        AnswersSpecificQuestion GetAnswersSpecificQuestionById(int id);

        AnswersSpecificQuestion UpdateAnswersSpecificQuestionById(AnswersSpecificQuestion answersGenericQuestion);

        void DeleteAnswersSpecificQuestionById(AnswersSpecificQuestion answersSpecificQuestion);

        void DeleteSpecificQuestion(SpecificQuestion specificQuestion);        

    }
}
