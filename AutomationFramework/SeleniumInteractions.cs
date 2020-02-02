using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using Xunit.Abstractions;

namespace AutomationFramework
{
    public enum BrowserType
    {
        Chrome,
        FireFox,
        InternetExplorer
    }

    public partial class SeleniumInteractions : IDisposable
    {
        private readonly IWebDriver _driver;
        private readonly ITestOutputHelper _testOutputHelper;


        public SeleniumInteractions(ITestOutputHelper testOutputHelper, BrowserType browserType = BrowserType.Chrome)
        {
            _testOutputHelper = testOutputHelper;
            _driver = OpenBrowser(browserType);
        }

        private IWebDriver OpenBrowser(BrowserType browserType)
        {
            _testOutputHelper.WriteLine($"   Opening browser {browserType.ToString()}");

            IWebDriver driver = null;

            switch (browserType)
            {
                case BrowserType.Chrome:
                {
                    driver = new ChromeDriver();
                    break;
                }

                case BrowserType.FireFox:
                {
                    driver = new FirefoxDriver();
                    break;
                }

                case BrowserType.InternetExplorer:
                {
                    driver = new InternetExplorerDriver();
                    break;
                }
            }

            if (driver == null)
            {
                throw new Exception($"Wrong browser type passed `{browserType.ToString()}`");
            }

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            driver.Manage().Window.Maximize();

            return driver;
        }


        public void SetValue(By selector, string value)
        {
            _driver.FindElement(selector).SendKeys(value);
            _testOutputHelper.WriteLine($"    Setting value to '{selector.ToString()}' with '{value}'");
        }

        public void Click(By selector)
        {
            _driver.FindElement(selector).Click();
            _testOutputHelper.WriteLine($"    Clicked '{selector.ToString()}'");
        }


        public void NavigateToUrl(string url)
        {
            _driver.Navigate().GoToUrl(url);
            _testOutputHelper.WriteLine($"    Opening '{url}'");
        }

        /// <summary>
        /// Use this method to write custom output
        /// </summary>
        /// <param name="action"></param>
        public void WriteLine(Action<ITestOutputHelper> action)
        {
            action(_testOutputHelper);
        }

        public void Dispose()
        {
            _driver.Quit();
        }
    }
}