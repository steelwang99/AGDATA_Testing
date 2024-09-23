using AGDATAUIAutomationTest.Base;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AGDataUITests.Pages
{
    public class CompanyOverviewPage : BasePage
    {

        private readonly By valuesSection = By.TagName("span");
        private readonly By letUsGetStartedButton = By.XPath("//a[contains(text(),\"Let's Get Started\")]");

        public CompanyOverviewPage(IWebDriver driver) : base(driver) { }

        public List<string> GetValues()
        {
            
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            var valuesElements = _driver.FindElements(valuesSection);
            return valuesElements.Select(e => e.Text).ToList();
        }

        public void ClickLetsGetStarted()
        {
            ClickElement(letUsGetStartedButton);
        }
    }
}
