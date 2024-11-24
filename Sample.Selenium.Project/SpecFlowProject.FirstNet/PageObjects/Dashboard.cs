using DevExpress.DirectX.Common.Direct3D;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;
using TechTalk.SpecFlow.Configuration.JsonConfig;

namespace SpecFlowProject.FirstNet.PageObjects
{
    public class Dashboard
    {
        private IWebDriver _driver;
        public Dashboard(IWebDriver driver)
        {

            _driver = driver;

        }
        IWebElement DropDown => _driver.FindElement(By.XPath("//*[@id=\"header_container\"]/div[2]/div/span/select"));
        IWebElement _CartOpion => _driver.FindElement(By.XPath("//*[@id=\"shopping_cart_container\"]"));
        IWebElement _CheckOut => _driver.FindElement(By.Id("checkout"));
        public void displayCheapItemsFirst()
        {
            SelectElement options = new SelectElement(DropDown);

            options.SelectByText("Price (low to high)");
        }

        public void addCheapItems(int number)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            wait.Until(d => d.FindElement(By.CssSelector("#inventory_container")));

            var addToCart = _driver.FindElements(By.XPath("//button[contains(@class,'btn btn_primary btn_small btn_inventory ')]"));



            for (int i = 0; i < number; i++)
            {
                try
                {
                    var addButton = addToCart[i];
                    addButton.Click();

                    Console.WriteLine($"added product {i + 1} to the cart");
                }
                catch (NoSuchElementException)
                {

                    Console.WriteLine($"Unable to find 'add to cart' button for product {i + 1} to the cart");
                }
                finally
                {
                    //Content
                }
            }

        }

        public void Cart(int CartProducts)
        {
            _CartOpion.clickElement();

            IReadOnlyList<IWebElement> productsInCart = _driver.FindElements(By.ClassName("cart_item"));
            Assert.AreEqual(CartProducts, productsInCart.Count());

        }

        public void adjustCart(int MaintainTotalCost)
        {

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.FindElement(By.XPath("//div[contains(@class,'cart_list')]")));

            //Find all the Items in the cart
            ReadOnlyCollection<IWebElement> priceOfProduct = _driver.FindElements(By.XPath("//div[contains(@class,'item_pricebar')]//div[contains(@class,'inventory_item_price')]"));


            List<decimal> prices = new List<decimal>();

            foreach (var priceElement in priceOfProduct)
            {
                string priceText = priceElement.Text.Trim('$').Trim();
                if (decimal.TryParse(priceText, out decimal price))
                {
                    prices.Add(price);
                }
            }

            //Sum of cost of all the items
            decimal totalCost = prices.Sum();

            //repeat and remove the items from the cart untill the total reaches to 50 or less than 50
            while (totalCost >= MaintainTotalCost && prices.Count > 0)
            {
                try
                {
                    ReadOnlyCollection<IWebElement> removeButton = _driver.FindElements(By.XPath("//div[contains(@class,'item_pricebar')]//button[contains(@class,'btn btn_secondary btn_small cart_button')]"));
                    removeButton[0].Click();

                    wait.Until(d => d.FindElements(By.XPath("//div[contains(@class,'cart_item')]")).Count != prices.Count);

                    priceOfProduct = _driver.FindElements(By.XPath("//div[contains(@class,'item_pricebar')]//div[contains(@class,'inventory_item_price')]"));
                    prices.Clear();

                    foreach (var priceElement in priceOfProduct)
                    {
                        string priceText = priceElement.Text.Trim();
                        if (!string.IsNullOrEmpty(priceText))
                        {
                            priceText = priceText.Replace("$", "").Trim();
                            if (decimal.TryParse(priceText, out decimal price))
                            {
                                prices.Add(price);
                            }
                        }
                    }
                        totalCost = prices.Sum();

                   
                }
                //incase if we get stale exception and unable to locate the element from DOM
                catch (StaleElementReferenceException)
                {

                    priceOfProduct = _driver.FindElements(By.XPath("//div[contains(@class,'item_pricebar')]//div[contains(@class,'inventory_item_price')]"));
                   
                }
               

            }
        }

        public void checkoutCart()
        {
          _CheckOut.clickElement();
        }

    }
}
