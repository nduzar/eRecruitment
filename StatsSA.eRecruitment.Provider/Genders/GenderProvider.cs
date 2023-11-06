using StatsSA.eRecruitment.IProvider.Genders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.UnitOfWork;

namespace StatsSA.eRecruitment.Provider.Genders
{
    public class GenderProvider : IGenderProvider
    {
        private IUnitOfWork _unitOfWork;
        public GenderProvider(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Gender AddGender(Gender gender)
        {
            _unitOfWork.GenderManager.AddGender(gender);
            _unitOfWork.SaveChanges();
            return gender;
        }

        public IEnumerable<Gender> GetAllGenders()
        {
            return _unitOfWork.GenderManager.GetAllGenders();
        }
        public Gender GetGenderById(int genderId)
        {
            return _unitOfWork.GenderManager.GetGenderById(genderId);
        }
    }
}
