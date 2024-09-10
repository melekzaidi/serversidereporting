using CommandeClientCleanArch.Reporting.PredefinedReports;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerSideApp.PredefinedReports
{
    public static class ReportsFactory
    {
        public static Dictionary<string, Func<XtraReport>> Reports = new Dictionary<string, Func<XtraReport>>()
        {
            ["Report1"] = () => new Report1(),

            ["Bonjour"] = () => new BonjourReport(),
            ["TestReport"] = () => new TestReport(),
            ["TestExportReport"] = () => new TestExportReport()
        };
    }
}