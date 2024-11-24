using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Utils;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow.Assist;

namespace SpecFlowProject.FirstNet.PageObjects
{
    internal class LoginPage
    {
        private IWebDriver _driver;
        public LoginPage(IWebDriver driver)
        {

            _driver = driver;
        }

        IWebElement _userName => _driver.FindElement(By.Id("user-name"));
        IWebElement _Password =>_driver.FindElement(By.Id("password"));
        IWebElement _loginButton => _driver.FindElement(By.Id("login-button"));
        IWebElement _ElementInDashboard => _driver.FindElement(By.XPath("//*[@id=\"header_container\"]/div[2]/span"));



        public void LoginWithCredentials(string username, string password)
        {
            _userName.input(username);
            _Password.input(password);
            _loginButton.clickElement();

            Thread.Sleep(3000);
        }
        public void ErrorValidation() {

            string ExpectedlError = "Epic sadface: Username and password do not match any user in this service";

            //Identify the text and compare with the actual error 
            var actualError = _driver.FindElement(By.XPath("//*[@id=\"login_button_container\"]/div/form/div[3]/h3")).Text;
            Console.WriteLine(actualError);
            Assert.AreEqual(ExpectedlError, actualError);

        }

        public void dashboard() 
        {
            if (!string.IsNullOrEmpty(_ElementInDashboard.Text))
            {
                Console.WriteLine("The text is present in the dashboard so dashboard has loaded successfully");
            }
        }
           

    }
}
