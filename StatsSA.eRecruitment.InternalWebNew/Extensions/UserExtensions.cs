using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Hosting;

namespace StatsSA.eRecruitment.InternalWeb.Extensions
{
    public static class UserExtensions
    {
        public static int GetUserId(this IPrincipal User)
        {
            var identity = (ClaimsIdentity)User.Identity;
            int userId=  -1;
            if (identity.Claims.Any())
            {
                userId = int.Parse(identity.Claims.Where(c => c.Type == "id").Single().Value);
            }
            return userId;
        }
        public static string GetUserEmail (string UserName)
        {
            try
            {
                using (var ctx = new PrincipalContext(ContextType.Domain, "treasury.gov.za"))
                {
                    using (var qbeUser = new UserPrincipal(ctx))
                    {
                        qbeUser.SamAccountName = UserName;
                        // create your principal searcher passing in the QBE principal    
                        PrincipalSearcher srch = new PrincipalSearcher(qbeUser);

                        // find all matches
                        var user = srch.FindOne();
                        return user.UserPrincipalName;
                    }
                };

            }
            catch(Exception ex)
            {

            }
            return null;
                      
        }

        public static List<ADSamAccount> SearchUsers(string partialString)
        {
            List<ADSamAccount> persons = new List<ADSamAccount>();
            using (DirectoryEntry searchRoot = new DirectoryEntry("LDAP://TREASURY.GOV.ZA/OU=National Treasury,DC=Treasury,DC=gov,DC=za"))
            {
                using (DirectorySearcher directorySearcher = new DirectorySearcher(searchRoot))
                {
                    directorySearcher.Filter = "(&(objectCategory=person)(objectClass=user))";
                    directorySearcher.PageSize = 1000;
                    directorySearcher.PropertiesToLoad.Add("CN");
                    directorySearcher.PropertiesToLoad.Add("sAMAccountName");
                    directorySearcher.PropertiesToLoad.Add("displayName");
                    directorySearcher.PropertiesToLoad.Add("userPrincipalName");
                    using (SearchResultCollection all = directorySearcher.FindAll())
                    {
                        foreach (SearchResult searchResult in all.Cast<SearchResult>().ToList<SearchResult>())
                        {
                            try
                            {
                                if (searchResult.Properties["displayname"][0].ToString().ToLower().Contains(partialString.ToLower()))
                                {
                                    using (DirectoryEntry directoryEntry = new DirectoryEntry(searchResult.Path))
                                    {
                                        var person = new ADSamAccount()
                                        {
                                            samAccountname = directoryEntry.Properties["sAMAccountName"].Value.ToString(),
                                            fullNames = directoryEntry.Properties["displayName"].Value.ToString(),
                                            emailAddress = directoryEntry.Properties["userPrincipalName"].Value.ToString()
                                        };
                                        persons.Add(person);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                }
            }
            return persons;
        }
        //public static List<ADSamAccount> SearchUsers(string partialString)
        //{

        //    // create your domain context
        //    PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "treasury.gov.za");

        //    // define a "query-by-example" principal - here, we search for a UserPrincipal 
        //    UserPrincipal qbeUser = new UserPrincipal(ctx);
        //    qbeUser.SamAccountName = partialString + "*";


        //    // create your principal searcher passing in the QBE principal    
        //    PrincipalSearcher srch = new PrincipalSearcher(qbeUser);

        //    // find all matches
        //    List<ADSamAccount> persons = new List<ADSamAccount>();
        //    if(partialString.Length > 3)
        //    {
        //        foreach (var found in srch.FindAll())
        //        {
        //            string str_samaccountname = found.SamAccountName;
        //            string str_fullnames = found.DisplayName;
        //            string str_emailaddress = found.UserPrincipalName;
        //            var person = new ADSamAccount()
        //            {
        //                samAccountname = str_samaccountname,
        //                fullNames = str_fullnames,
        //                emailAddress = str_emailaddress
        //            };
        //            persons.Add(person);
        //        }
        //    }
        //    return persons;
        //}
    }
    public class ADSamAccount
    {
        public string samAccountname { get; set; }
        public string fullNames { get; set; }
        public string emailAddress { get; set; }
    }
}