using StatsSA.eRecruitment.IProvider.Applications;
using StatsSA.eRecruitment.IProvider.VerApplicantProfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.UnitOfWork;
using StatsSA.eRecruitment.Entities.Enums;

namespace StatsSA.eRecruitment.Provider.Applications
{
    public class ApplicationProvider : IApplicationProvider, IVerApplicantProfileProvider //Why is ApplicationProvider inheriting from IVerApplicantProfileProvider??
    {
        private IUnitOfWork _unitOfWork;
        public ApplicationProvider(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Application AddApplication(Application ApplicationToAdd)
        {
            VerApplicantProfile versionProfile = new VerApplicantProfile();
            //ApplicantProfile currentProfile = this._unitOfWork.ApplicantProfileManager.GetApplicantProfileProfileId(ApplicationToAdd.)
            ApplicationToAdd.ApplicationStatusId = (int)Entities.Enums.ApplicationStatuses.Received;
            ApplicationToAdd.ApplicationStatusDate = DateTime.Now;
            ApplicationToAdd.MaintDate = DateTime.Now;
            ApplicationToAdd.ApplicationRequirement.MaintDate = DateTime.Now;
            ApplicationToAdd.ApplicationRequirement.MaintUser = ApplicationToAdd.MaintUser;
            _unitOfWork.BeginTransaction();
            _unitOfWork.ApplicationManager.AddApplication(ApplicationToAdd);
            _unitOfWork.SaveChanges();
            _unitOfWork.CommitTransaction();
            return ApplicationToAdd;
        }

        public IEnumerable<Application> GetApplicantApplication(int userId)
        {
            var ProfileId = _unitOfWork.ApplicantProfileManager.GetProfileIdByUserId(userId);
            var VersionProfiles = this.GetVerAppProfilesById(ProfileId);
            List<Application> applications = new List<Application>();
            foreach (VerApplicantProfile profile in VersionProfiles)
            {
                var OldApplications = _unitOfWork.ApplicationManager.GetApplicantApplications(profile.VerApplicantProfileId);
                foreach(Application application in OldApplications)
                {
                    applications.Add(application);
                } 
            }
            return applications;
        }

        public void UpdateApplication(Application ApplicationToUpdate)
        {
            var application = _unitOfWork.ApplicationManager.GetApplicationById(ApplicationToUpdate.ApplicationId);
            //application.Requirement = this.GetRequirement(ApplicationToUpdate.ApplicationId);
            application.MaintUser = ApplicationToUpdate.MaintUser;
            application.MaintDate = ApplicationToUpdate.MaintDate;

            //application.Requirement.IsSouthAfricanCitizen = ApplicationToUpdate.Requirement.IsSouthAfricanCitizen;
            //application.Requirement.HighestQualification = ApplicationToUpdate.Requirement.HighestQualification;
            //application.Requirement.Age = ApplicationToUpdate.Requirement.Age;
            //application.Requirement.HasKnowledge = ApplicationToUpdate.Requirement.HasKnowledge;
            //application.Requirement.DoneInternship = ApplicationToUpdate.Requirement.DoneInternship;
            //application.Requirement.MaintDate = ApplicationToUpdate.Requirement.MaintDate;
            //application.Requirement.MaintUser = ApplicationToUpdate.Requirement.MaintUser;

            _unitOfWork.BeginTransaction();
            _unitOfWork.ApplicationManager.UpdateApplication(application);
            _unitOfWork.SaveChanges();
            _unitOfWork.CommitTransaction();
        }

        public IEnumerable<Application> GetRankedApplications(int vacancyId)
        {
           // var applications = _unitOfWork.ApplicationManager.GetRankedApplications(vacancyId);
            //foreach(Application application in applications)
            //{
            //    application.ApplicationRequirement = _unitOfWork.ApplicationRequirementManager.GetApplicationRequirementById(application.ApplicationId);
            //}
            return _unitOfWork.ApplicationManager.GetRankedApplications(vacancyId);
        }

        public Application GetApplicantApplication(int vacancyId, int applicationId)
        {
            return _unitOfWork.ApplicationManager.GetRankedApplications(vacancyId).Where(x => x.ApplicationId == applicationId).FirstOrDefault();
        }

        public IEnumerable<ListOfApplication> GetAllApplications(int vacancyId)
        {
            return _unitOfWork.ApplicationManager.GetAllApplications(vacancyId);
        }

        public IEnumerable<Application> GetApplicationByEmail(int vacancyId, string email)
        {
            var applications = _unitOfWork.ApplicationManager.GetApplicationByEmail(vacancyId, email).Where(x => x.MaintUser == email);
            List<Application> list = new List<Application>();

            foreach (Application application in applications)
            {
                //application.Requirement = _unitOfWork.RequirementManager.GetRequirementById(application.ApplicationId);
                list.Add(application);
            }
            //return list.OrderByDescending(x=> x.Requirement.OverallAverage).Take(20);
            return list;
        }

        /*  Get applications based on the programme for internship  */
        //public IEnumerable<Application> GetAllRecievedApplications(int vacancyId, int programmeId, out int noApplications)
        //{
        //    noApplications = 0;
        //    var applications = _unitOfWork.ApplicationManager.GetAllRecievedApplications(vacancyId, out noApplications);
        //    List<Application> list = new List<Application>();

        //    foreach (Application application in applications)
        //    {
        //        application.ApplicationRequirement = _unitOfWork.ApplicationRequirementManager.GetApplicationRequirementById(application.ApplicationId);
        //        list.Add(application);
        //    }

        //    return list.Where(x => x.ApplicationRequirement.ProgrammeId == programmeId).OrderByDescending(x => x.ApplicationRequirement.OverallAverage).Take(20);
        //}



        public Requirement GetRequirement(int applicationID)
        {
            return _unitOfWork.RequirementManager.GetRequirementById(applicationID);
        }
        public void UpdateApplicationStatus(Application ApplicationToUpdate)
        {
            ApplicationToUpdate.MaintDate = DateTime.Now;
           // _unitOfWork.BeginTransaction();
            _unitOfWork.ApplicationManager.UpdateApplicationStatus(ApplicationToUpdate);
            _unitOfWork.SaveChanges();
            //_unitOfWork.CommitTransaction();
        }

        public VerApplicantProfile AddVerApplicantProfile(VerApplicantProfile profile)
        {
            throw new NotImplementedException();
        }

        public VerApplicantProfile GetVerAppProfileById(int VerApplicantProfileId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VerApplicantProfile> GetVerAppProfilesById(int ApplicantProfileId)
        {
            return _unitOfWork.VerApplicantProfileManager.GetVerAppProfilesById(ApplicantProfileId);
        }

        public IEnumerable<Application> GetApplicantApplication(IEnumerable<int> verApplicantProfileIds)
        {
            return _unitOfWork.ApplicationManager.GetApplicantApplication(verApplicantProfileIds);
        }

        public IEnumerable<VerApplicantProfile> GetVerAppProfileByUserId(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
