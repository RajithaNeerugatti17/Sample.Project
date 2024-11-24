using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using OpenQA.Selenium;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CsvHelper;

namespace SpecFlowProject.FirstNet.Utility
{
    public class ExtentReport
    {
        public static ExtentReports _extentReports;
        public static ExtentTest _feature;
        public static ExtentTest _scenario;

        //public static string directory = AppDomain.CurrentDomain.BaseDirectory;
        //public static String testResultPath = dir.Replace("bin\\Debug\\net6.0", "TestResults");
        public static string ssParentDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
        public static string testResultsPath = Path.Combine(ssParentDirectory, "results");


        public static void ExtentReportInit()
        {
            var htmlReporter = new ExtentHtmlReporter(testResultsPath);
            htmlReporter.Config.ReportName = "Automation Status Report";
            htmlReporter.Config.DocumentTitle = "Automation Status Report";
            htmlReporter.Config.Theme = Theme.Standard;
            htmlReporter.Start();

            _extentReports = new ExtentReports();
            _extentReports.AttachReporter(htmlReporter);
            _extentReports.AddSystemInfo("Application", "FirstNet");
            _extentReports.AddSystemInfo("Browser", "Chrome");
            _extentReports.AddSystemInfo("OS", "Windows");
        }

        public static void ExtentReportTearDown()
        {
            _extentReports.Flush();
        }

        public string addScreenshot(IWebDriver driver, ScenarioContext scenarioContext)
        {


            string timeStamp = DateTime.Now.ToString("ddMMyyyy-HHmmss");
            string ssFilename = $"{scenarioContext.ScenarioInfo.Title}_{timeStamp}";

            if (!Directory.Exists(testResultsPath))
            {
                Directory.CreateDirectory(testResultsPath);
            }

            string ssFilePath = Path.Combine(testResultsPath, ssFilename + ".png");
            Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile(ssFilePath);

            return ssFilePath;
        }
       
    }
}
