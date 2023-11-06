using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IProvider.AnswerThreshold
{
    public interface IAnswersThresholdProvider
    {
        void AddThreshold(AnswersThreshold answersThreshold);

        AnswersThreshold GetAnswersThreshold(int vacancyId);

        AnswersThreshold UpdateAnswersThreshold(AnswersThreshold answersThreshold);
    }
}
