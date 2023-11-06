using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IProvider.SpecificQuestions
{
    public interface ISpecificQuestionsProvider
    {

        SpecificQuestion AddSpecificQuestions(SpecificQuestion specificQuestions);

        IEnumerable<SpecificQuestion> GetAllQuestions(int vacancyId);

        SpecificQuestion GetSpecificQuestionById(int id);

        SpecificQuestion UpdateSpecificQuestionById(SpecificQuestion specificQuestion);
    }
}
