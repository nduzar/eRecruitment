using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StatsSA.eRecruitment.Entities;

namespace StatsSA.eRecruitment.WebApi.Models
{
    public class SpecificQuestionsAnswers
    {

        public SpecificQuestion SpecificQuestion { get; set; }

        public List<AnswersSpecificQuestion> ListAnswersSpecificQuestions { get; set; }
    }
}