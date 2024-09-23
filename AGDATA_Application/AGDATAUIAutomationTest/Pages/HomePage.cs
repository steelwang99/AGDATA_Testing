using AGDATAUIAutomationTest.Base;
using OpenQA.Selenium;

namespace AGDataUITests.Pages
{
    public class HomePage : BasePage
    {
        private readonly By companyMenu = By.XPath("//*[@id=\"menu-item-992\"]");
        private readonly By overviewMenu = By.XPath("//*[@id=\"menu-item-829\"]/a");

        public HomePage(IWebDriver driver) : base(driver) { }

        public void OpenCompanyOverview()
        {
            // Hover over "Company" and then click "Overview"
            ClickElement(companyMenu);
            ClickElement(overviewMenu);
        }
    }
}
