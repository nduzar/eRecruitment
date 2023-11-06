using StatsSA.eRecruitment.IManager.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.Manager.GenericRepository;
using StatsSA.eRecruitment.IManager.UnitOfWork;

namespace StatsSA.eRecruitment.Manager.Profiles
{
    public class ApplicantProfileManager : IApplicantProfileManager
    {
        private readonly eRecruitmentEntities _context;
        public ApplicantProfileManager(eRecruitmentEntities context)
        {
            _context = context;
        }

        public ApplicantProfile AddApplicantProfile(ApplicantProfile profile)
        {
            return _context.ApplicantProfiles.Add(profile);
        }

        public ApplicantProfile GetApplicantProfileByIdNo(string idNumber)
        {
            return _context.ApplicantProfiles.FirstOrDefault(p => p.IdentityNumber == idNumber);
        }

        public ApplicantProfile UpdateApplicantProfileByIdNo(ApplicantProfile appliacantProfile)
        {
            throw new NotImplementedException();
        }

        public ApplicantProfile GetApplicantProfileByUserId(int userId)
        {
            return _context.ApplicantProfiles.FirstOrDefault(p => p.UserId == userId);            
        }
        public int GetProfileIdByUserId(int userId)
        {
            var Profile = _context.ApplicantProfiles.Where(a => a.UserId == userId).OrderByDescending(a => a.ApplicantProfileId).FirstOrDefault();
            int ProfileId = Profile.ApplicantProfileId;
            return ProfileId;
        }

        public ApplicantProfile UpdateApplicantProfile(ApplicantProfile appliacantProfile)
        {
            _context.ApplicantProfiles.Attach(appliacantProfile);
            _context.Entry(appliacantProfile).State = System.Data.Entity.EntityState.Modified;
            return appliacantProfile;
        }
        public void AddApplicantQualification(ApplicantQualification qualification)
        {
            _context.ApplicantQualifications.Add(qualification);
        }
        public IEnumerable<ApplicantQualification> GetApplicantQualification(int ApplicantProfileId)
        {
            var allQualifications = _context.ApplicantQualifications.Where(qual => qual.ApplicantProfileId == ApplicantProfileId).ToList();
            return allQualifications;
        }
        public bool CheckIfIdNumberExists(string IdNumber)
        {
            return _context.ApplicantProfiles.FirstOrDefault(p=>p.IdentityNumber == IdNumber) != null;
        }
        public ApplicantProfile GetApplicantProfileProfileId(int profileId)
        {
            return _context.ApplicantProfiles.FirstOrDefault(p => p.ApplicantProfileId == profileId);
        }
    }

}
