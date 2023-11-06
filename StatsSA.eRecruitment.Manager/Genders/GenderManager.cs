using StatsSA.eRecruitment.IManager.Genders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.Manager.GenericRepository;

namespace StatsSA.eRecruitment.Manager.Genders
{
    public class GenderManager : IGenderManager
    {
        private eRecruitmentEntities _context;
        public GenderManager(eRecruitmentEntities context)
        {
            _context = context;
        }
        public Gender AddGender(Gender gender)
        {
            return _context.Genders.Add(gender);
        }

        public IEnumerable<Gender> GetAllGenders()
        {
            return _context.Genders.ToList();
        }

        public Gender GetGenderById(int genderId)
        {
            return _context.Genders.FirstOrDefault(gender => gender.GenderId == genderId);
        }
    }
}
