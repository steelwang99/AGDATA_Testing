using AGDataUITests.Pages;
using AGDATAUIAutomationTest.Base;
using NUnit.Framework;
using System.Collections.Generic;


namespace AGDataUITests
{
    public class Test_01_CompanyOverview : AGDataTestsBase
    {
        public Test_01_CompanyOverview(BrowserType browserType) : base(browserType) { }

        [Test]
        public void VerifyCompanyOverviewFlow()
        {
            // Navigate to home page
            _driver.Navigate().GoToUrl("https://www.agdata.com");

            // Initialize page objects
            var homePage = new HomePage(_driver);
            var companyOverviewPage = new CompanyOverviewPage(_driver);

            //Open Company > Overview
            homePage.OpenCompanyOverview();

            //Get all values on the overview page and imput them to a list
            List<string> valuesList = companyOverviewPage.GetValues();
            Assert.IsNotEmpty(valuesList, "Values list should not be empty");

            // Log all values
            TestContext.Progress.WriteLine("Values on the Overview Page:");
            foreach (var value in valuesList)
            {
                TestContext.Progress.WriteLine(value);
            }

            //Click "Let's Get Started"
            companyOverviewPage.ClickLetsGetStarted();

            //Validate the contact page is displayed
            var contactPage = new ContactPage(_driver);
            Assert.IsTrue(contactPage.IsLoaded(), "Contact page should be loaded.");
        }
    }
}
