using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;

namespace StatsSA.eRecruitment.IManager.AnswerThreshold
{
    public interface IAnswersThresholdManager
    {
        void AddThreshold(AnswersThreshold answersThreshold);

        AnswersThreshold GetAnswersThreshold(int vacancyId);

        AnswersThreshold UpdateAnswersThreshold(AnswersThreshold answersThreshold);
    }
}
