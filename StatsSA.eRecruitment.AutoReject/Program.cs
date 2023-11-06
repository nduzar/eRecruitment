using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatsSA.eRecruitment.Provider.Applications;
using StatsSA.eRecruitment.Entities;
using System.IO;
using System.Globalization;
using StatsSA.eRecruitment.Manager;
using StatsSA.eRecruitment.Entities.Enums;
using System.Data;
using System.Data.SqlClient;

namespace StatsSA.eRecruitment.AutoReject
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Server=CENVMSQLP03\PROD01;Database=Dev_eRecruitment;Trusted_Connection=True;";

            string query = @"SELECT ApplicationId,a.VacancyId,a.VerApplicantProfileId,ApplicationStatusId, ApplicationStatusDate, a.MaintDate, a.MaintUser,FirstNames, Surname, v.ClosingDate
                                FROM [Dev_eRecruitment].[dbo].[Applications] a
                                inner join VerApplicantProfiles vA
                                on vA.VerApplicantProfileId = a.VerApplicantProfileId
                                inner join Vacancies v
                                on v.VacancyId = a.VacancyId
                                WHERE ApplicationStatusId = 6
                                order by 1";

            DataTable table = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(table);
                }
            }

            foreach (DataRow row in table.Rows)
            {
                var applicationId = row["ApplicationId"].ToString();
                DateTime closingDate = Convert.ToDateTime(row["ClosingDate"].ToString());

                if (closingDate.Date.AddDays(40) == DateTime.Today.Date)
                {

                    var directories = Directory.GetDirectories(Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\")));

                    foreach (var item in directories)
                    {
                        if (item.Contains("Template"))
                        {
                            var fileName = Directory.GetFiles(item);
                            var path = fileName[0];
                            var body = CreateEmailBody(row["FirstNames"].ToString() + " " + row["Surname"].ToString(), path);

                            Helper.SendEmail(row["MaintUser"].ToString(), "eRecruitment - Application Unsuccessful", body);
                        }
                    }                    

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        query = "UPDATE [Dev_eRecruitment].[dbo].[Applications] SET ApplicationStatusId = '5' WHERE ApplicationId = " + applicationId;
                        SqlCommand command = new SqlCommand(query, connection);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        private static string CreateEmailBody(string fullname, string mappedPath)
        {
            string body = string.Empty;

            using (StreamReader reader = new StreamReader(mappedPath))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{name}", fullname);
            return body;
        }
    }
}
