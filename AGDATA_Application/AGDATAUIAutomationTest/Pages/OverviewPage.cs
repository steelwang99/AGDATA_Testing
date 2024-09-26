using AGDATAUIAutomationTest.Base;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace AGDataUITests.Pages
{
    public class CompanyOverviewPage : BasePage
    {

        private readonly By valuesSection = By.TagName("span");
        private readonly By letUsGetStartedButton = By.XPath("//a[contains(text(),\"Let's Get Started\")]");
        private readonly By valuesPayouts = By.XPath("//span[contains(text(),'3.70')]");

        public CompanyOverviewPage(IWebDriver driver) : base(driver) { }

        public List<string> GetValues()
        {
            _wait.Until(ExpectedConditions.ElementExists(valuesPayouts));
            var valuesElements = _driver.FindElements(valuesSection);
            return valuesElements.Select(e => e.Text).ToList();
        }

        public void ClickLetsGetStarted()
        {
            ClickElement(letUsGetStartedButton);
        }
    }
}
