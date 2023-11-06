using StatsSA.eRecruitment.IManager.QualificationTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.Manager.GenericRepository;

namespace StatsSA.eRecruitment.Manager.QualificationTypes
{
    public class QualificationTypeManager : IQualificicationTypeManager
    {
        private eRecruitmentEntities _context;
        public QualificationTypeManager(eRecruitmentEntities context)
        {
            _context = context;
        }
        public IEnumerable<QualificationType> GetQualificationType()
        {
            return _context.QualificationTypes.ToList();
        }
        public QualificationType GetQualificationTypeById(int qualificationTypeId)
        {
            return _context.QualificationTypes.FirstOrDefault(qualType => qualType.QualificationTypeId == qualificationTypeId);
        }
    }
}
