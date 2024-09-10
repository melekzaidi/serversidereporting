using CommandeClientCleanArch.Reporting.Data;
using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace CommandeClientCleanArch.Reporting.PredefinedReports
{
    public partial class Report1 : DevExpress.XtraReports.UI.XtraReport
    {
        public Report1()
        {
            InitializeComponent();
            var ds = new Dsprocessus();
            var dt = new DataTable();
            dt.Columns.Add("DataColumn1");
            dt.Columns.Add("DataColumn2");
            dt.Columns.Add("DataColumn3");

            var dr = dt.NewRow();
            dr["DataColumn1"] = "aa";
            dr["DataColumn2"] = "bb";
            dr["DataColumn3"] = "cc";
            dt.Rows.Add(dr);

            // Check if 'processus' table exists and update it
            if (ds.Tables.Contains("processus"))
            {
                ds.Tables["processus"].Clear();  // Clear existing rows
                ds.Tables["processus"].Merge(dt);  // Merge new data
            }

            // Set the DataSource of the report
            this.DataSource = ds;

            
        }
    }
}
