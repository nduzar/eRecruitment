using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StatsSA.eRecruitment.WebApi.Models
{
    public class QuestionsAnswers
    {
        public List<SpecificQuestionsAnswers> ListSpecificQuestionsAnswers { get; set; }

        public List<AnswersGenericQuestion> ListGenericQuestionsAnswers { get; set; }

        public AnswersThreshold AnswersThreshold { get; set; }
    }
}