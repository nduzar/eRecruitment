using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IManager.AnswersSpecificQuestions
{
    public interface IAnswersSpecificQuestionsManager
    {
        void AddAnswersSpecificQuestion(List<AnswersSpecificQuestion> answersSpecificQuestion);

        void UpdateAnswersSpecificQuestion(AnswersSpecificQuestion answerSpecificQuestion);

        IEnumerable<AnswersSpecificQuestion> GetAllAnswersSpecificQuestion(int questionId);

        AnswersSpecificQuestion GetAnswersSpecificQuestionById(int id);

        void DeleteAnswersSpecificQuestion(AnswersSpecificQuestion answersSpecificQuestion);

        void DeleteSpecificQuestion(SpecificQuestion specific);
    }
}
