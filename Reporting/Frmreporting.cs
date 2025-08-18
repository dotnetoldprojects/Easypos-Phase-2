using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reporting
{
    public partial class Frmreporting : Form
    {
        private ReportDocument currentReport;
        public Frmreporting()
        {
            InitializeComponent();
        }
        public void LoadReport(ReportDocument report)
        {
            CleanUpReport(); // نظف أي تقرير قديم
            currentReport = report;
            CRV.ReportSource = currentReport;
            CRV.Refresh();
        }
        private void CleanUpReport()
        {
            if (currentReport != null)
            {
                try
                {
                    CRV.ReportSource = null;
                    currentReport.Close();
                    currentReport.Dispose();
                    currentReport = null;
                }
                catch { }
            }
        }
        private void Frmreporting_FormClosed(object sender, FormClosedEventArgs e)
        {
            CleanUpReport();
            CRV.Dispose();
        }
    }
}
