using StatsSA.eRecruitment.IManager.VerProfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.Manager.GenericRepository;
using StatsSA.eRecruitment.IManager.UnitOfWork;

namespace StatsSA.eRecruitment.Manager.VerProfiles
{
    public class VerApplicantProfileManager : IVerApplicantProfileManager
    {
        private readonly eRecruitmentEntities _context;
        public VerApplicantProfileManager(eRecruitmentEntities context)
        {
            _context = context;
        }

        public VerApplicantProfile GetVerAppProfileById(int VerApplicantProfileId)
        {

            return _context.VerApplicantProfiles.Where(verProfile => verProfile.VerApplicantProfileId == VerApplicantProfileId).FirstOrDefault();
           // return base.Get(verProfile => verProfile.VerApplicantProfileId == VerApplicantProfileId);
        }

        public IEnumerable<VerApplicantProfile> GetVerAppProfileByUserId(int userId)
        {
            return _context.VerApplicantProfiles.Where(verProfile => verProfile.UserId == userId).ToList();
            //return base.GetMany(verProfile => verProfile.UserId == userId).ToList();
        }

        public IEnumerable<VerApplicantProfile> GetVerAppProfilesById(int ApplicantProfileId)
        {
            return _context.VerApplicantProfiles.Where(verProfile => verProfile.ApplicantProfileId == ApplicantProfileId).ToList();
            //return base.GetMany(verProfile => verProfile.ApplicantProfileId == ApplicantProfileId);
        }
    }
        
}
