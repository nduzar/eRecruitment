using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StatsSA.eRecruitment.Reports
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'ListCandidatesDataSet.PRC_LIST_CANDIDATES_PER_POST' table. You can move, or remove it, as needed.
            this.PRC_LIST_CANDIDATES_PER_POSTTableAdapter.Fill(this.ListCandidatesDataSet.PRC_LIST_CANDIDATES_PER_POST, new DateTime(2018, 01, 01), new DateTime(2018, 12, 23), "All");
            this.reportViewer1.LocalReport.ReportPath = "../../rptListCandidatePerPost.rdlc";
            this.reportViewer1.RefreshReport();
        }
    }
}