using StatsSA.eRecruitment.IProvider.QualificationTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.UnitOfWork;

namespace StatsSA.eRecruitment.Provider.QualificationTypes
{
    public class QualificationTypeProvider : IQualificationTypeProvider
    {
        private IUnitOfWork _unitOfWork;
        public QualificationTypeProvider(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<QualificationType> GetAllQaulificationTypes()
        {
            return _unitOfWork.QualificicationTypeManager.GetQualificationType();
        }

        public QualificationType GetQualificationTypeById(int qualificationTypeId)
        {
            return _unitOfWork.QualificicationTypeManager.GetQualificationTypeById(qualificationTypeId);
        }
    }
}
