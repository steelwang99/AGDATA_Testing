using AGDataUITests.Pages;
using AGDATAUIAutomationTest.Base;


namespace AGDataUITests
{
    public class Test_01_CompanyOverview : AGDataTestsBase
    {
        public Test_01_CompanyOverview(BrowserType browserType) : base(browserType) { }

        [Test]
        public void Test_01_VerifyCompanyOverviewFlow()
        {
            // Navigate to home page
            extent_test.Value.Info("Navigating to home page...");
            _driver.Navigate().GoToUrl("https://www.agdata.com");


            // Initialize page objects
            var homePage = new HomePage(_driver);
            var companyOverviewPage = new CompanyOverviewPage(_driver);

            //Open Company > Overview
            extent_test.Value.Info("Openning Overview page...");
            homePage.OpenCompanyOverview();

            //Get all values on the overview page and imput them to a list
            extent_test.Value.Info("Getting all values on the overview page and imput them to a list.");
            List<string> valuesList = companyOverviewPage.GetValues();
            Assert.IsNotEmpty(valuesList, "Values list should not be empty");

            // Log all values
            TestContext.Progress.WriteLine("Values on the Overview Page:");
            foreach (var value in valuesList)
            {
                if(!string.IsNullOrEmpty(value))
                { 
                    TestContext.Progress.WriteLine(value);
                    extent_test.Value.Info(value);
                }
            }

            //Click "Let's Get Started"
            extent_test.Value.Info("Openning Let's Get Started page...");
            companyOverviewPage.ClickLetsGetStarted();

            //Validate the contact page is displayed
            extent_test.Value.Info("Openning contact page...");
            var contactPage = new ContactPage(_driver);
            Assert.IsTrue(contactPage.IsLoaded(), "Contact page should be loaded.");
        }
    }
}
