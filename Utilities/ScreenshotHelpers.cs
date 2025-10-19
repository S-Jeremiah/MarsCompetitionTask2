using OpenQA.Selenium;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.Extensions;

using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;



namespace MARSCOMPETITION.Utilities
{
    public static class ScreenshotHelpers
    {
        public static string CaptureScreenshot(IWebDriver driver, string fileName)
        {
            string projectDir = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)!.Parent!.Parent!.Parent!.FullName;
            string folderPath = Path.Combine(projectDir, "Screenshots");
            if (driver == null)
                throw new ArgumentNullException(nameof(driver), "WebDriver instance cannot be null.");
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string fullPath = Path.Combine(folderPath, $"{fileName}_{timestamp}.png");

            Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile(fullPath);

            return fullPath;

        }

        /* public static MediaEntityModelProvider CaptureScreenshot(IWebDriver driver, string screenshotName)
         {
             if (driver == null)
                 throw new ArgumentNullException(nameof(driver), "WebDriver instance cannot be null.");

             // Take screenshot as Base64
             ITakesScreenshot ts = (ITakesScreenshot)driver;
             Screenshot screenshot = ts.GetScreenshot();
             string base64Screenshot = screenshot.AsBase64EncodedString;

             // Attach to ExtentReports
             return MediaEntityBuilder.CreateScreenCaptureFromBase64String(base64Screenshot).Build();
         }*/
    }
}

