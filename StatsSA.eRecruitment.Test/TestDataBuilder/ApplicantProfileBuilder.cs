using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.Test.TestDataBuilder
{
    public class ApplicantProfileBuilder
    {
        private string Surname = "SuperSurname";
        private string FirstNames = "SuperUserFirstNames";
        private DateTime DateOfBirth = DateTime.Now;
        private string IdentityNumber = "8705236527070";
        private int RaceId = ObjectMother.RaceTestData.African.RaceId;
        private int GenderId = ObjectMother.GenderTestData.Male.GenderId;
        private bool Disability = false;
        private bool Citizen = true;
        private string Nationality = "South African";
        private bool WorkPermit;
        private bool PreviousDismissal = false;
        private bool CriminalOffence = false;
        private DateTime OccupationRegistrationDate;
        private string OccupationRegistrationDesc;
        private string MaintUser = "TestUser";
        private DateTime MaintDate = DateTime.Now;

        public ApplicantProfileBuilder WithSurname(string surname)
        {
            this.Surname = surname;
            return this;
        }
        public ApplicantProfileBuilder WithFirstNames(string firstNames)
        {
            this.FirstNames = firstNames;
            return this;
        }
        public ApplicantProfileBuilder WithDateOfBirth(DateTime dateOfBirth)
        {
            this.DateOfBirth = dateOfBirth;
            return this;
        }
        public ApplicantProfileBuilder WithIdentityNumber(string idnumber)
        {
            this.IdentityNumber = idnumber;
            return this;
        }
        public ApplicantProfileBuilder WithRace(Race race)
        {
            this.RaceId = race.RaceId;
            return this;
        }
        public ApplicantProfileBuilder WithGender(Gender gender)
        {
            this.GenderId = gender.GenderId;
            return this;
        }
        public ApplicantProfileBuilder WithDisability(bool disability)
        {
            this.Disability = disability;
            return this;
        }
        public ApplicantProfileBuilder WithCitizen(bool isCitizen)
        {
            this.Citizen = isCitizen;
            return this;
        }
        public ApplicantProfileBuilder WithNationality(string nationality)
        {
            this.Nationality = nationality;
            return this;
        }
        public ApplicantProfileBuilder WithWorkPermit(bool hasWorkPermit)
        {
            this.WorkPermit = hasWorkPermit;
            return this;
        }
        public ApplicantProfileBuilder WithPreviousDismissal(bool previousDismissal)
        {
            this.PreviousDismissal = previousDismissal;
            return this;
        }
        public ApplicantProfileBuilder WithCriminalOffence(bool criminalOffence)
        {
            this.CriminalOffence = criminalOffence;
            return this;
        }
        public ApplicantProfileBuilder WithOccupationRegistrationDate(DateTime occupationRegistrationDate)
        {
            this.OccupationRegistrationDate = occupationRegistrationDate;
            return this;
        }
        public ApplicantProfileBuilder WithOccupationRegistrationDesc(string occupationRegistrationDesc)
        {
            this.OccupationRegistrationDesc = occupationRegistrationDesc;
            return this;
        }

        public ApplicantProfile Build(User user)
        {
            return new ApplicantProfile
            {
                UserId = user.Id,
                Surname = this.Surname,
                FirstNames = this.FirstNames,
                DateOfBirth = this.DateOfBirth,
                IdentityNumber = this.IdentityNumber,
                RaceId = this.RaceId,
                GenderId = this.GenderId,
                Disability = this.Disability,
                Citizen = this.Citizen,
                Nationality = this.Nationality,
                WorkPermit = this.WorkPermit,
                PreviousDismissal = this.PreviousDismissal,
                CriminalOffence = this.CriminalOffence,
                OccupationRegistrationDate = this.OccupationRegistrationDate,
                OccupationRegistrationDesc = this.OccupationRegistrationDesc,
                MaintUser = this.MaintUser,
                MaintDate = this.MaintDate
            };
        }
    }
}
