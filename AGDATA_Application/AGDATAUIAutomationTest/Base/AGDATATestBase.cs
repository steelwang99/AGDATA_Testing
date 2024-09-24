using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework.Interfaces;

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

        public ExtentReports extent;
        public ExtentTest test;
        public ThreadLocal<ExtentTest> extent_test = new();

        public AGDataTestsBase(BrowserType browserType)
        {
            _browserType = browserType;
        }

        [SetUp]
        public void SetUp()
        {
            //Initialize the report folder
            string dir = System.Environment.CurrentDirectory;
            string? projdir = Directory.GetParent(dir)?.Parent?.Parent?.FullName;
            string reporpath = projdir + "\\Reports\\Report.html";
            var htmlreport = new ExtentHtmlReporter(reporpath);
            extent = new ExtentReports();
            extent.AttachReporter(htmlreport);

            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            extent_test.Value = test;

            // Initialize the browser based on the parameter passed
            switch (_browserType)
            {
                case BrowserType.Chrome:
                    _driver = new ChromeDriver();
                    extent_test.Value.Info("The Chrome browser is being used.");
                    break;
                case BrowserType.Firefox:
                    _driver = new FirefoxDriver();
                    extent_test.Value.Info("The Firefox browser is being used.");
                    break;
                case BrowserType.Edge:
                    _driver = new EdgeDriver();
                    extent_test.Value.Info("The Edge browser is being used.");
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
        public void SetTestResults()
        {
            System.Threading.Thread.Sleep(100);
            var status = TestContext.CurrentContext.Result.Outcome.Status;

            if (status == TestStatus.Failed)
            {
                var messageTrace = TestContext.CurrentContext.Result.Message;
                var stackTrace = TestContext.CurrentContext.Result.StackTrace;
                DateTime date = DateTime.Now;
                string Filename = "Screenshot_" + date.ToString("h_mm_ss") + ".png";
                extent_test?.Value?.Fail(messageTrace);
                extent_test?.Value?.Fail("TestCase Status : Failed", CaptureScreenShot(_driver, Filename));
                extent_test?.Value?.Fail(stackTrace);
            }
            else if (status == TestStatus.Passed)
            {
                if (extent_test?.Value != null)
                {
                    extent_test.Value.Log(Status.Pass, MarkupHelper.CreateLabel("TestCase Status : " + status, ExtentColor.Green));
                }
                else
                {
                    TestContext.Progress.WriteLine("extent_test.Value is null");
                }
            }
        }
        public static MediaEntityModelProvider CaptureScreenShot(IWebDriver driver, string screenShotName)
        {
            ITakesScreenshot scr = (ITakesScreenshot)driver;
            var screenshot = scr.GetScreenshot().AsBase64EncodedString;

            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, screenShotName).Build();
        }

        [TearDown]
        public void TearDown()
        {

            SetTestResults();

            if (_driver != null)
            {
                _driver.Quit();
                _driver.Dispose();
            }
            extent.Flush();
        }
    }
}
