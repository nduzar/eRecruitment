using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.UnitOfWork;
using StatsSA.eRecruitment.IProvider.Institutions;

namespace StatsSA.eRecruitment.Provider.Institutions
{
    public class InstitutionProvider : IInstitutionProvider
    {
        private IUnitOfWork _unitOfWork;
        public InstitutionProvider(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Institution AddInstitution(Institution institution)
        {
            _unitOfWork.InstitutionsManager.AddInstitution(institution);
            _unitOfWork.SaveChanges();
            return institution;
        }

        public IEnumerable<Institution> GetAllInstitutions()
        {
            return _unitOfWork.InstitutionsManager.GetAllInstitutions();
        }
        public Institution GetInstitutionById(int institutionId)
        {
            return _unitOfWork.InstitutionsManager.GetInstitutionById(institutionId);
        }
    }
}
