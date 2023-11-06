using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StatsSA.eRecruitment.WebApi.Models
{
    public class GenericQuestionsAnswers
    {
        public GenericQuestion GenericQuestion { get; set; }

        public List<AnswersGenericQuestion> ListAnswersGenericQuestions { get; set; }
    }
}