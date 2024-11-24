using DevExpress.Internal;
using FluentAssertions.Equivalency;
using Microsoft.Extensions.Hosting;
using NUnit.Framework.Internal.Execution;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V127.DOM;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;

namespace SpecFlowProject.FirstNet
{
    public static class ExtensionCustom
    {
        public static void input(this IWebElement locator, string text)
        {
            locator.Clear();
            locator.SendKeys(text);
        }

        public static void clickElement(this IWebElement locator)
        {
            locator.Click();
        }

        public static void dropworn(this IWebElement locator, string values)
        {
            SelectElement element = new SelectElement(locator);
            element.SelectByValue(values);
        }

        public static void multiDropworn(this IWebElement locator, string[] values)
        {
            SelectElement element = new SelectElement(locator);
            foreach (var value in values)
            {
                element.SelectByValue(value);
                Console.WriteLine(value);
            }

        }
        public static bool DropdownOptionAvailability(SelectElement selectElement, string optionTextToCheck)
        {
            IList<IWebElement> elementOptions = selectElement.Options;
            foreach (var option in elementOptions)
            {
                if (option.Text == optionTextToCheck)
                    return true;
            }
            return false;
        }

        public static void verify(this IWebElement locator)
        {
            try
            {
                if (locator.Displayed)
                {
                    Console.WriteLine("verified");
                }
                else
                {
                    Console.WriteLine("element unIdentified");
                }
            }
            catch (NoSuchElementException)
            {
                throw new Exception("No element find");
            }

        }

        public static IWebElement WaitForElement(IWebDriver driver, By by, TimeSpan timeOut)
        {
            WebDriverWait wait = new WebDriverWait(driver, timeOut);
            try
            {
                return wait.Until(drv => drv.FindElement(by));
            }
            catch (TimeoutException)
            {
                Console.WriteLine($"Element not found in {timeOut}");
                return null;
            }

        }

    }
}
