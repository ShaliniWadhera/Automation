using AutomationFramework;
using OpenQA.Selenium;
using Shouldly;

namespace JavaScriptAlerts.Actions
{

    public enum AlertType
    {
        Alert,
        Confirm,
        Prompt
    }
    
    class AlertActions
    {
        private readonly SeleniumInteractions _selenium;

        public AlertActions(SeleniumInteractions selenium)
        {
            _selenium = selenium;
        }

        public void ClickForJsAlert(AlertType alertType)
        {
            var clickForJsAlert = By.XPath($"//*[@id='content'] //button[text()='Click for JS {alertType.ToString()}']");

            _selenium.Click(clickForJsAlert);
        }

        public void VerifyAlertResult(string alertText)
        {
            var resultText = By.Id("result");

            var element = _selenium.GetWebElement(resultText);
            element.Text.ShouldBeEquivalentTo(alertText, "Results are not matched");
            _selenium.WriteLine((helper) => helper.WriteLine($"    '{alertText}' verified"));
        }
    }
}
