using System;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using Shouldly;
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
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(15);
            driver.Manage().Window.Maximize();

            return driver;
        }


        public void SetValue(By selector, string value)
        {
            if (!IsElementReady(selector))
            {
                throw new Exception($"Element is not ready to set value {selector.ToString()}");
            }


            _driver.FindElement(selector).Clear();
            _driver.FindElement(selector).SendKeys(value);
            _testOutputHelper.WriteLine($"    Setting value to '{selector.ToString()}' with '{value}'");
        }

        public void Click(By selector)
        {
            if (!IsElementReady(selector))
            {
                throw new Exception($"Element is not ready to click {selector.ToString()}");
            }

            _driver.FindElement(selector).Click();
            _testOutputHelper.WriteLine($"    Clicked '{selector.ToString()}'");
        }

        public void SelectText(By selector, string text)
        {
            if (!IsElementReady(selector))
            {
                throw new Exception($"Element is not ready to select {selector.ToString()}");
            }

            var element = _driver.FindElement(selector);
            var selectElement = new SelectElement(element);

            selectElement.SelectByText(text);
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
        
        public ReadOnlyCollection<IWebElement> GetWebElements(By selector)
        {
            return _driver.FindElements(selector);
        }
        
        public IWebElement GetWebElement(By selector)
        {
            return _driver.FindElement(selector);
        }

        public bool IsElementReady(By selector)
        {
            var element = _driver.FindElement(selector);
            return element.Displayed && element.Enabled;
        }

        public void Dispose()
        {
            _driver.Quit();
        }
    }
}