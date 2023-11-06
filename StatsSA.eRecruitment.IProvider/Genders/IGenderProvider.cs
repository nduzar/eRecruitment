using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IProvider.Genders
{
    public interface IGenderProvider
    {
        Gender AddGender(Gender gender);

        IEnumerable<Gender> GetAllGenders();

        Gender GetGenderById(int genderId);
    }
}
