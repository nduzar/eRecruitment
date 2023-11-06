using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IProvider.GenericQuestions
{
    public interface IGenericQuestionProvider
    {
        IEnumerable<GenericQuestion> GetAllQuestions();

        void DeleteGenericQuestion(GenericQuestion question);

        void SaveAllQuestions(List<GenericQuestion> genericQuestions);
    }
}
