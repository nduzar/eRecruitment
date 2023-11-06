using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StatsSA.eRecruitment.WebApi.Models
{
    public class ApplicationModels
    {
        public List<SpecificQuestionsAnswers> SpecificQuestionsAnswers { get; set; }

        public List<GenericQuestionsAnswers> GenericQuestionsAnswers { get; set; }

        public Application Application { get; set; }

        public AnswersThreshold AnswersThreshold { get; set; }
    }
}