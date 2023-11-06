using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StatsSA.eRecruitment.Entities;

namespace StatsSA.eRecruitment.InternalWeb.Extensions
{
    public static class RankingExtensions
    {
        public static  int CalculateScore(ListOfApplication aplReq)
        {
            if (aplReq != null)
            {
                //return (aplReq.SouthAfricanCitizenScore + aplReq.QualificationScore + aplReq.AgeScore + aplReq.KnowledgeScore + aplReq.InternshipScore);
                return 0;
            }
            else
            {
                return (0);
            }
            
        }
    }
}