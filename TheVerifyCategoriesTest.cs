using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    [TestFixture]
    public class VerifyCategories
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;
        
        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            baseURL = "http://demo.litecart.net/";
            verificationErrors = new StringBuilder();
        }
        
        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }
        
        [Test]
        public void TheVerifyCategoriesTest()
        {
            driver.Navigate().GoToUrl(baseURL + "/en/");
            Assert.AreEqual("http://demo.litecart.net/en/rubber-ducks-c-1/", driver.FindElement(By.CssSelector("nav#site-menu li.category-1 a")).GetAttribute("href"));
            Assert.AreEqual("Rubber Ducks", driver.FindElement(By.CssSelector("nav#site-menu li.category-1 a")).Text);
            driver.FindElement(By.LinkText("Rubber Ducks")).Click();
            Assert.IsTrue(IsElementPresent(By.CssSelector("li.product a.link")));
            Assert.AreEqual("http://demo.litecart.net/en/this-n-that-c-2/", driver.FindElement(By.CssSelector("nav#site-menu li.category-2 a")).GetAttribute("href"));
            Assert.AreEqual("This n' that", driver.FindElement(By.CssSelector("nav#site-menu li.category-2 a")).Text);
            driver.FindElement(By.LinkText("This n' that")).Click();
            Assert.IsFalse(IsElementPresent(By.CssSelector("li.product a.link")));
        }
        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        
        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }
        
        private string CloseAlertAndGetItsText() {
            try {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert) {
                    alert.Accept();
                } else {
                    alert.Dismiss();
                }
                return alertText;
            } finally {
                acceptNextAlert = true;
            }
        }
    }
}
