namespace StatsSA.eRecruitment.Reports
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.ListCandidatesDataSet = new StatsSA.eRecruitment.Reports.ListCandidatesDataSet();
            this.PRC_LIST_CANDIDATES_PER_POSTBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.PRC_LIST_CANDIDATES_PER_POSTTableAdapter = new StatsSA.eRecruitment.Reports.ListCandidatesDataSetTableAdapters.PRC_LIST_CANDIDATES_PER_POSTTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.ListCandidatesDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PRC_LIST_CANDIDATES_PER_POSTBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource3.Name = "ListCandidatesDataSet";
            reportDataSource3.Value = this.PRC_LIST_CANDIDATES_PER_POSTBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "StatsSA.eRecruitment.Reports.rptListCandidatePerPost.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(682, 386);
            this.reportViewer1.TabIndex = 0;
            // 
            // ListCandidatesDataSet
            // 
            this.ListCandidatesDataSet.DataSetName = "ListCandidatesDataSet";
            this.ListCandidatesDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // PRC_LIST_CANDIDATES_PER_POSTBindingSource
            // 
            this.PRC_LIST_CANDIDATES_PER_POSTBindingSource.DataMember = "PRC_LIST_CANDIDATES_PER_POST";
            this.PRC_LIST_CANDIDATES_PER_POSTBindingSource.DataSource = this.ListCandidatesDataSet;
            // 
            // PRC_LIST_CANDIDATES_PER_POSTTableAdapter
            // 
            this.PRC_LIST_CANDIDATES_PER_POSTTableAdapter.ClearBeforeFill = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 386);
            this.Controls.Add(this.reportViewer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ListCandidatesDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PRC_LIST_CANDIDATES_PER_POSTBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource PRC_LIST_CANDIDATES_PER_POSTBindingSource;
        private ListCandidatesDataSet ListCandidatesDataSet;
        private ListCandidatesDataSetTableAdapters.PRC_LIST_CANDIDATES_PER_POSTTableAdapter PRC_LIST_CANDIDATES_PER_POSTTableAdapter;
    }
}

