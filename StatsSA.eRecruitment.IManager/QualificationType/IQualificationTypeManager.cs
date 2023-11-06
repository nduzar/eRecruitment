using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IManager.QualificationTypes
{
    public interface IQualificicationTypeManager
    {
        IEnumerable<QualificationType> GetQualificationType();
        QualificationType GetQualificationTypeById(int qualificationTypeId);
    }
}
