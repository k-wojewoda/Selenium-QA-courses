using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using OpenQA.Selenium.Remote;

namespace Selenium_QA_Courses
{
    [TestFixture]
    public class MyFirstTest
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        void CheckMenuPoint(string MenuPoint, string HeaderText)
        {
            driver.FindElement(By.XPath("//a[contains(@href, '"+ MenuPoint+"')]")).Click();
            IList<IWebElement> header1 = driver.FindElements(By.XPath("//h1[text()=' "+ HeaderText + "']")).ToList();
            Boolean isPresent = header1.Count > 0;
            Assert.AreEqual(isPresent, true);
        }
        void CheckSecondMenuPoint(string MenuPoint, string HeaderText)
        {
            driver.FindElement(By.XPath("(//a[contains(@href, '" + MenuPoint + "')])[2]")).Click();
            IList<IWebElement> header1 = driver.FindElements(By.XPath("//h1[text()=' " + HeaderText + "']")).ToList();
            Boolean isPresent = header1.Count > 0;
            Assert.AreEqual(isPresent, true);
        }




        [SetUp]
        public void start()
        {
            //RUNNING TEST REMOTLY IN CLOUD - browserstack
            //string USERNAME = "katarzyna48";
            //string AUTOMATE_KEY = "WLck2foedkTh5ohLEEoX";

            //DesiredCapabilities caps = new DesiredCapabilities();

            //caps.SetCapability("os", "Windows");
            //caps.SetCapability("os_version", "10");
            //caps.SetCapability("browser", "Chrome");
            //caps.SetCapability("browser_version", "80");
            //caps.SetCapability("browserstack.user", USERNAME);
            //caps.SetCapability("browserstack.key", AUTOMATE_KEY);
            //caps.SetCapability("name", "katarzyna48's First Test");

            //driver = new RemoteWebDriver(
            //  new Uri("https://hub-cloud.browserstack.com/wd/hub/"), caps
            //);

            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void FirstTest()
        {
            driver.Url = "http://www.rp.pl/";
            wait.Until(ExpectedConditions.TitleContains("Rp.pl"));
        }

        [Test]
        public void Assignment07()
        {
            //LOGIN
            driver.Url = "http://localhost:8080/litecart/public_html/admin/";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();

            //MENU WALK THROUGH
            CheckMenuPoint("appearance", "Template");
            CheckSecondMenuPoint("appearance", "Template");
            CheckMenuPoint("logotype", "Logotype");
            CheckMenuPoint("catalog", "Catalog");
            CheckSecondMenuPoint("catalog", "Catalog");
            CheckMenuPoint("product_groups", "Product Groups"); 
            CheckMenuPoint("option_groups", "Option Groups"); 
            CheckMenuPoint("manufacturers", "Manufacturers"); 
            CheckMenuPoint("suppliers", "Suppliers"); 
            CheckMenuPoint("delivery_statuses", "Delivery Statuses");
            CheckMenuPoint("sold_out_statuses", "Sold Out Statuses"); 
            CheckMenuPoint("quantity_units", "Quantity Units"); 
            CheckMenuPoint("csv", "CSV Import/Export"); 
            CheckMenuPoint("countries", "Countries");
            CheckMenuPoint("currencies", "Currencies");
            CheckMenuPoint("customers", "Customers");
            CheckSecondMenuPoint("customers", "Customers");
            CheckMenuPoint("csv", "CSV Import/Export"); 
            CheckMenuPoint("newsletter", "Newsletter"); 
            CheckMenuPoint("geo_zones", "Geo Zones"); 
            CheckMenuPoint("languages", "Languages");
            CheckSecondMenuPoint("languages", "Languages"); 
            CheckMenuPoint("storage_encoding", "Storage Encoding");
            CheckMenuPoint("modules", "Job Modules");
            CheckSecondMenuPoint("modules", "Job Modules"); 
            CheckMenuPoint("modules&doc=customer", "Customer Modules");
            CheckMenuPoint("shipping", "Shipping Modules");
        }

        [Test]
        public void Assignment09()
        {
            //ASSIGNMENT 09.1
            //selects 5th column of a table 
            //table[@class='dataTable']/tbody//tr/td[5]

            //ASSIGNMENT 09.2 -----------------------------------------------------------

            //LOGIN
            driver.Url = "http://localhost:8080/litecart/public_html/admin/?app=geo_zones&doc=edit_geo_zone&page=1&geo_zone_id=2";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();


            //select selected options
            //table[@class='dataTable']/tbody//tr/td[3]//option[@selected='selected']
            IList<IWebElement> zones = driver.FindElements(By.XPath("//table[@class='dataTable']/tbody//tr/td[3]//option[@selected='selected']")).ToList();
            IList<String> zoneNames = new List<String>();
            foreach(IWebElement zone in zones)
            {
                zoneNames.Add(zone.GetAttribute("Text"));
            }
            List<String> sortedZones = new List<String>(zoneNames);
            sortedZones.Sort();
            Boolean equal = zoneNames.SequenceEqual(sortedZones);
            Assert.AreEqual(equal, true);
        }

        [Test]
        public void Assignment10()
        {
            //LOGIN
            driver.Url = "http://localhost:8080/litecart/public_html/en/";

            //grey, stroke out price
            IWebElement oldPrice = driver.FindElement(By.XPath("//div[@id='box-campaigns']//s[@class='regular-price']"));
            String color = oldPrice.GetCssValue("color");           
            Assert.AreEqual(color, "rgba(119, 119, 119, 1)");
            String textDecor = oldPrice.GetCssValue("text-decoration-line");        
            Assert.AreEqual(textDecor, "line-through");
            String fontWeight = oldPrice.GetCssValue("font-weight");
            Assert.AreEqual(fontWeight, "400");

            //new red bold price            
            IWebElement newPrice = driver.FindElement(By.XPath("//div[@id='box-campaigns']//strong[@class='campaign-price']"));
            String colorNEW = newPrice.GetCssValue("color");
            Assert.AreEqual(colorNEW, "rgba(204, 0, 0, 1)");
            String textDecorNEW = newPrice.GetCssValue("text-decoration-line");
            Assert.AreEqual(textDecorNEW, "none");
            String fontWeightNEW = newPrice.GetCssValue("font-weight");
            Assert.AreEqual(fontWeightNEW, "700");
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
