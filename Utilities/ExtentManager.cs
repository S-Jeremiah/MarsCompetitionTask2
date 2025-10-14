using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MARSCOMPETITION.Utilities
{
    public static class ExtentManager
    {
        private static ExtentReports? _extent;
        private static ExtentSparkReporter?_sparkReporter;

        public static ExtentReports GetExtent()
        {
            if (_extent == null)
            {
                string projectDir = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)!.Parent!.Parent!.Parent!.FullName;
                string reportDir = Path.Combine(projectDir, "Report");
                Directory.CreateDirectory(reportDir);
                string reportPath = Path.Combine(reportDir, "report.html");
                Console.WriteLine("=== Extent Report Initialization ===");

                _sparkReporter = new ExtentSparkReporter(reportPath);
               _extent = new ExtentReports();
                _extent.AttachReporter(_sparkReporter);
                _extent.AddSystemInfo("Tester", "Shirley");
                _extent.AddSystemInfo("Environment", "QA");

            }

            return _extent;
        }
    }
}
