using OpenQA.Selenium;
using Sample_Selenium_Project.Utility;
using SpecFlowProject.FirstNet.PageObjects;
using System.Net.Mail;

namespace SpecFlowProject.FirstNet.StepDefinitions
{
    [Binding]
    public sealed class StepDefinition
    {
        private readonly IWebDriver _driver;
        public StepDefinition(IWebDriver driver)
        {
            _driver = driver;   
        }
        [Given(@"I Launch saucedemo url")]
        public void GivenILaunchsaucedemoUrl()
        {
            if (Hooks1.Config.Environment == "T1")
            {
                _driver.Navigate().GoToUrl("https://www.saucedemo.com/ ");
            }

            else if (Hooks1.Config.Environment == "T2")
            {
                _driver.Navigate().GoToUrl("");
            }

        }
   

        [When(@"I login to saucedemo using username '([^']*)'and password '([^']*)'")]
        public void WhenILoginTosaucedemoUsingUsernameAndPassword(string username, string password)
        {
            LoginPage loginWithCred = new LoginPage(_driver);
            loginWithCred.LoginWithCredentials(username, password);
        }

        [Then(@"the login will fail and show the error")]
        public void ThenTheLoginWillFailAndShowTheError()
        {
            LoginPage loginPage = new LoginPage(_driver);
            loginPage.ErrorValidation();
        }

        [Then(@"I should see the dashboard")]
        public void ThenIShouldSeeTheDashboard()
        {
            LoginPage Dashboard = new LoginPage(_driver);
            Dashboard.dashboard();
        }
        [Then(@"I select the Items from low price")]
        public void ThenISelectTheItemsFromLowPrice()
        {
            Dashboard CheapItems = new Dashboard(_driver);
            CheapItems.displayCheapItemsFirst();
        }

        [Then(@"I add the first (.*) cheapest items into the cart")]
        public void ThenIAddTheFirstCheapestItemsIntoTheCart(int number)
        {
            Dashboard Products = new Dashboard(_driver);
            Products.addCheapItems(number);
      
        }
        [Then(@"I Navigate to cart and see (.*) items in the cart")]
        public void ThenINavigateToCartAndSeeItemsInTheCart(int cartCount)
        {
            Dashboard Products = new Dashboard(_driver);
            Products.Cart(cartCount);
        }

        [Then(@"I check the total cost and remove products untill the total cost will be less than (.*)")]
        public void ThenICheckTheTotalCostAndRemoveProductsUntillTheTotalCostWillBeLessThan(int TotalCost)
        {
            Dashboard ProductCost = new Dashboard(_driver);
            ProductCost.adjustCart(TotalCost);
        }
        [Then(@"I click on checkout")]
        public void ThenIClickOnCheckout()
        {
           Dashboard checkout = new Dashboard(_driver);
            checkout.checkoutCart();
        }







    }
}
