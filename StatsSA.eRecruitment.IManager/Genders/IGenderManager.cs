using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IManager.Genders
{
    public interface IGenderManager
    {
        Gender AddGender(Gender gender);

        IEnumerable<Gender> GetAllGenders();

       Gender GetGenderById(int genderId);
    }
}
