using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;

namespace AGDATAUIAutomationTest.Base
{
    public enum BrowserType
    {
        Chrome,
        Firefox,
        Edge
    }
    // Implement paralle testing on different browsers
    [TestFixture(BrowserType.Chrome)]
    [TestFixture(BrowserType.Firefox)]
    [TestFixture(BrowserType.Edge)]   
    [Parallelizable(ParallelScope.Fixtures)]
    public class AGDataTestsBase
    {
        protected IWebDriver _driver;
        protected WebDriverWait _wait;
        private BrowserType _browserType;

        public AGDataTestsBase(BrowserType browserType)
        {
            _browserType = browserType;
        }

        [SetUp]
        public void SetUp()
        {
            // Initialize the browser based on the parameter passed
            switch (_browserType)
            {
                case BrowserType.Chrome:
                    _driver = new ChromeDriver();
                    break;
                case BrowserType.Firefox:
                    _driver = new FirefoxDriver();
                    break;
                case BrowserType.Edge:
                    _driver = new EdgeDriver();
                    break;
                default:
                    throw new ArgumentException("Browser not supported");
            }
            // Implicit wait
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            // Explicit wait
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            _driver.Manage().Window.Maximize();

        }

        [TearDown]
        public void TearDown()
        {
            if (_driver != null)
            {
                _driver.Quit();
                _driver.Dispose();
            }
        }
    }
}
