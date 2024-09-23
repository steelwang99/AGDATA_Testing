using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AGDATAUIAutomationTest.Base
{
    public abstract class BasePage
    {
        protected IWebDriver _driver;
        protected WebDriverWait _wait;

        public BasePage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        protected IWebElement FindElement(By by)
        {
            return _wait.Until(d => d.FindElement(by));
        }

        protected void ClickElement(By by)
        {
            FindElement(by).Click();
        }
    }
}
