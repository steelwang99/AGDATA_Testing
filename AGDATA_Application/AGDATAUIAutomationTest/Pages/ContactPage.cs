using AGDATAUIAutomationTest.Base;
using OpenQA.Selenium;


namespace AGDataUITests.Pages
{
    public class ContactPage : BasePage
    {
        public ContactPage(IWebDriver driver) : base(driver) { }

        public bool IsLoaded()
        {
            // Verify by checking if the expected url is loaded.
            return _driver.Url.Contains("/contact");
        }
    }
}
