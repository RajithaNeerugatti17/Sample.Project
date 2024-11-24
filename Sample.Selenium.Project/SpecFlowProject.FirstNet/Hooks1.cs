using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using BoDi;
using DevExpress.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SpecFlowProject.FirstNet.Utility;
using TechTalk.SpecFlow;
using OfficeOpenXml;
using System.Text.Json;
using AventStack.ExtentReports.Configuration;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium.IE;
using System.Security.Cryptography.X509Certificates;

namespace Sample_Selenium_Project.Utility
{
    [Binding]
    public class Hooks1 : ExtentReport
    {
        private IObjectContainer _container;
        public static List<UserData> UserDataList;
        public static ConfigSetting Config;
        static string jsonFilePath = Directory.GetParent(@"../../../").FullName + Path.DirectorySeparatorChar + "Configuration/ConfigSetting.json";
        public Hooks1(IObjectContainer container)
        {
            _container = container;
        }
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            Config = new ConfigSetting();
            Console.WriteLine("content before test run");
            ExtentReportInit();
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(jsonFilePath);
            IConfigurationRoot configurationRoot = builder.Build();
            configurationRoot.Bind(Config);
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            Console.WriteLine("test run finished");
            ExtentReportTearDown();
        }
        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            _feature = _extentReports.CreateTest<Feature>(featureContext.FeatureInfo.Title);

        }

        [AfterFeature]
        public static void AfterFeature()
        {
            Console.WriteLine("after feature");

        }

        [BeforeScenario("@UAT")]
        public static void BeforeScenarioWithTag()
        {
            Console.WriteLine("UAT Scenario is executing...");
        }

        [BeforeScenario(Order = 1)]
        public void FirstBeforeScenario(ScenarioContext scenarioContext)
        {
            if (Config.Browser.ToLower() == "chrome")
            {
                IWebDriver driver = new ChromeDriver();
                driver.Manage().Window.Maximize();
                _container.RegisterInstanceAs(driver);
                _scenario = _feature.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);

                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            }
            else if (Config.Browser.ToLower() == "ie")
            {
                IWebDriver driver = new InternetExplorerDriver();
                driver.Manage().Window.Maximize();
                _container.RegisterInstanceAs(driver);
                _scenario = _feature.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);
            }
        }


        [AfterScenario]
        public void AfterScenario()
        {
            var driver = _container.Resolve<IWebDriver>();

            if (driver != null)
            {
                driver.Quit();
            }
        }

        [AfterStep]
        public void AfterStep(ScenarioContext scenarioContext)
        {
            Console.WriteLine("Running after step....");
            string stepType = scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
            string stepName = scenarioContext.StepContext.StepInfo.Text;

            var driver = _container.Resolve<IWebDriver>();

            //When scenario passed
            if (scenarioContext.TestError == null)
            {
                if (stepType == "Given")
                {
                    _scenario.CreateNode<Given>(stepName);
                }
                else if (stepType == "When")
                {
                    _scenario.CreateNode<When>(stepName);
                }
                else if (stepType == "Then")
                {
                    _scenario.CreateNode<Then>(stepName);
                }
                else if (stepType == "And")
                {
                    _scenario.CreateNode<And>(stepName);
                }


            }

            //When scenario fails
            if (scenarioContext.TestError != null)
            {

                if (stepType == "Given")
                {
                    _scenario.CreateNode<Given>(stepName).Fail(scenarioContext.TestError.Message,
                        MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenshot(driver, scenarioContext)).Build());
                }
                else if (stepType == "When")
                {
                    _scenario.CreateNode<When>(stepName).Fail(scenarioContext.TestError.Message,
                        MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenshot(driver, scenarioContext)).Build());
                }
                else if (stepType == "Then")
                {
                    _scenario.CreateNode<Then>(stepName).Fail(scenarioContext.TestError.Message,
                        MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenshot(driver, scenarioContext)).Build());
                }
                else if (stepType == "And")
                {
                    _scenario.CreateNode<And>(stepName).Fail(scenarioContext.TestError.Message,
                        MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenshot(driver, scenarioContext)).Build());
                }
            }
        }

    }
}