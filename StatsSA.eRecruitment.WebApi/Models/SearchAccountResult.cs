using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StatsSA.eRecruitment.WebApi.Models
{
    public class SearchAccountResult
    {
        public string IdentityNumber { get; set; }
        public string Email { get; set; }
        public string CellNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string HashedEmail { get; set; }
        public string HashedCellNumber { get; set; }
        public int UserId { get; set; }
    }
    public class SearchAccountResultByEmail
    {
        public string IdentityNumber { get; set; }
        public string Email { get; set; }
        public string HashedEmail { get; set; }
        public string SecondaryEmail { get; set; }
        public string HashedSecondaryEmail { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Otp { get; set; }
        
        public int Id { get; set; }
    }
    public class OtpRequestForEmail
    {
        public List<SearchAccountResultByEmail> SearchAccountResultByEmail { get; set; } = new List<SearchAccountResultByEmail>();
    }
}