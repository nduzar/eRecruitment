using Microsoft.Reporting.WebForms;
using StatsSA.eRecruitment.Entities;
using StatsSA.eRecruitment.IManager.UnitOfWork;
using StatsSA.eRecruitment.IProvider.Vacancies;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace StatsSA.eRecruitment.Provider.Vacancies
{
    public class VacancyProvider : IVacancyProvider
    {
        private IUnitOfWork _unitOfWork;
        public VacancyProvider(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Vacancy AddVacancy(Vacancy vacancy)
        {
            this._unitOfWork.BeginTransaction();
            var newVacancy = _unitOfWork.VacancyManager.AddVacancy(vacancy);
            this._unitOfWork.SaveChanges();
            this._unitOfWork.CommitTransaction();
            return newVacancy;
        }
        public IEnumerable<Vacancy> GetAllActiveVacancies()
        {
            return _unitOfWork.VacancyManager.GetAllActiveVacancies();
        }

        public Vacancy GetvacancyById(int vacancyId)
        {
            return _unitOfWork.VacancyManager.GetVacancyById(vacancyId);
        }
        public IEnumerable<Vacancy> GetCapturedVacancies()
        {
            var returnObj = _unitOfWork.VacancyManager.GetCapturedVacancies();
            return returnObj;
        }
        public IEnumerable<Vacancy> GetApprovedVacancies()
        {
            return _unitOfWork.VacancyManager.GetApprovedVacancies();
        }

        public IEnumerable<Vacancy> GetAuthorizedVacancies()
        {
            return _unitOfWork.VacancyManager.GetAuthorizedVacancies();
        }

        public IEnumerable<Vacancy> GetCancelledVacancies()
        {
            return _unitOfWork.VacancyManager.GetCancelledVacancies();
        }

        public IEnumerable<Vacancy> GetCancelableVacancies()
        {
            return _unitOfWork.VacancyManager.GetCancelableVacancies();
        }
        public IEnumerable<Vacancy> ChangeVacancyStatusApprover(Vacancy vacancy)
        {
            vacancy.MaintDate = DateTime.Now;
            vacancy.VacancyStatus = null;

            this._unitOfWork.BeginTransaction();
            _unitOfWork.VacancyManager.ChangeVacancyStatus(vacancy);
            this._unitOfWork.SaveChanges();
            this._unitOfWork.CommitTransaction();
            return this.GetCapturedVacancies();


        }

        public Vacancy UpdateVacancy(Vacancy vacancy)
        {
            vacancy.MaintDate = DateTime.Now;
            vacancy.VacancyStatus = null;

            this._unitOfWork.BeginTransaction();
            _unitOfWork.VacancyManager.ChangeVacancyStatus(vacancy);
            this._unitOfWork.SaveChanges();
            this._unitOfWork.CommitTransaction();
            return vacancy;
        }

        public IEnumerable<Vacancy> ChangeVacancyStatusAuthoriser(Vacancy vacancy)
        {
            vacancy.MaintDate = DateTime.Now;
            vacancy.VacancyStatus = null;

            this._unitOfWork.BeginTransaction();
            _unitOfWork.VacancyManager.ChangeVacancyStatus(vacancy);
            this._unitOfWork.SaveChanges();
            this._unitOfWork.CommitTransaction();
            return this.GetApprovedVacancies();


        }
        public IEnumerable<Vacancy> ChangeVacancyStatusCancelled(Vacancy vacancy)
        {
            vacancy.MaintDate = DateTime.Now;
            vacancy.VacancyStatus = null;

            this._unitOfWork.BeginTransaction();
            _unitOfWork.VacancyManager.ChangeVacancyStatus(vacancy);
            this._unitOfWork.SaveChanges();
            this._unitOfWork.CommitTransaction();
            return this.GetCancelableVacancies();


        }
        public IEnumerable<Vacancy> GetEditableVacancies()
        {
            return _unitOfWork.VacancyManager.GetEditableVacancies();
        }
        public IEnumerable<Vacancy> GetVacanciesForScreening()
        {
            return _unitOfWork.VacancyManager.GetVacanciesForScreening();
        }
        public Vacancy GetVacanyByVacancyId(int VacancyId)
        {
            return _unitOfWork.VacancyManager.GetVacanyByVacancyId(VacancyId);
        }


        public byte[] PrintToWord()
        {
            var approvedvacancies = _unitOfWork.VacancyManager.GetApprovedVacancies();
            //var filename = "ApprovedVacancies.doc";
            //string filePath = HttpContext.Current.Server.MapPath("~/Reports/" + filename);

            string ApprovedVacanciesInfo = "StatsSA.eRecruitment.InternalWeb.Reports.ApprovedVacanciesReport.rdlc";
            byte[] bytes = new byte[0];

            string binPath = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "bin");
            var assembly = Assembly.Load(System.IO.File.ReadAllBytes(binPath + "\\StatsSA.eRecruitment.InternalWeb.dll"));

            using (Stream stream = assembly.GetManifestResourceStream(ApprovedVacanciesInfo))
            {
                var viewer = new ReportViewer();
                viewer.LocalReport.EnableExternalImages = true;
                viewer.LocalReport.LoadReportDefinition(stream);

                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string filenameExtension;

                viewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", approvedvacancies));

                viewer.LocalReport.Refresh();
                
                viewer.ProcessingMode = ProcessingMode.Local;
                viewer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("~/bin/Reports/ApprovedVacanciesReport.rdlc");
                    // @"C:\tfs2015\eRecruitment\Dev\StatsSA.eRecruitment.InternalWeb\Reports\ApprovedVacanciesReport.rdlc";
                               
               bytes = viewer.LocalReport.Render("Word", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);

                //using (FileStream fs = new FileStream(filePath, FileMode.Create))
                //{
                //    fs.Write(bytes, 0, bytes.Length);
                //}
            }
            return bytes;

        }

        
        
        public static string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " Million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " Thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " Hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
                var tensMap = new[] { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }
    }
}
