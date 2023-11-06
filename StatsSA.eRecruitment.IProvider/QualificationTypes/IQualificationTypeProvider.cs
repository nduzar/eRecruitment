using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IProvider.QualificationTypes
{
    public interface IQualificationTypeProvider
    {
        IEnumerable<QualificationType> GetAllQaulificationTypes();
        QualificationType GetQualificationTypeById(int qualificationTypeId);
    }

}
