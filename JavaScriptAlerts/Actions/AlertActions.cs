using AutomationFramework;
using OpenQA.Selenium;

namespace JavaScriptAlerts.Actions
{
    class AlertActions
    {
        private readonly SeleniumInteractions _selenium;
        private const string HomeUrl = "https://localhost/";

        public AlertActions(SeleniumInteractions selenium)
        {
            _selenium = selenium;
        }

        public void ClickForJsAlertButton()
        {
            var clickForJsAlert = By.XPath("//*[@id='content'] //button[text()='Click for JS Alert']");

            _selenium.Click(clickForJsAlert);
        }

        public void ClickForJsConfirmButton()
        {
            var clickForJsConfirm = By.XPath("//*[@id='content'] //button[text()='Click for JS Confirm']");

            _selenium.Click(clickForJsConfirm);
        }
    }
}
