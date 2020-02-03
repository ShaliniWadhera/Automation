using System;
using AutomationFramework;
using JavaScriptAlerts.Actions;
using Xunit;
using Xunit.Abstractions;

namespace JavaScriptAlerts.Tests
{
    public class Alert : IDisposable
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private SeleniumInteractions _selenium;
        private AlertActions _alertAction;

        public Alert(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _selenium = new SeleniumInteractions(_testOutputHelper);
            _alertAction = new AlertActions(_selenium);
        }

        [Fact]
        public void CanVerifyJsAlert()
        {
            var testCase = new TestCase(_testOutputHelper, "Can verify JS Alert");

            testCase
                .ExecuteTestStep("Open Heroku App", () => _selenium.NavigateToUrl(Constants.JavaScriptAlertsUrl))
                .ExecuteTestStep("Click for JS alerts", () => _alertAction.ClickForJsAlert(AlertType.Alert))
                .ExecuteTestStep("Verify alert text", () => _selenium.HandleAlert("I am a JS Alert"))
                .ExecuteTestStep("Verify alert result", () => _alertAction.VerifyAlertResult($"You successfuly clicked an alert"))
                ;
        }

        [Fact]
        public void CanVerifyJsConfirm()
        {
            var testCase = new TestCase(_testOutputHelper, "Can verify JS Confirm");

            testCase
                .ExecuteTestStep("Open Heroku App", () => _selenium.NavigateToUrl(Constants.JavaScriptAlertsUrl))
                .ExecuteTestStep("Click for JS Confirm", () => _alertAction.ClickForJsAlert(AlertType.Confirm))
                .ExecuteTestStep("Verify alert text", () => _selenium.HandleAlert("I am a JS Confirm"))
                .ExecuteTestStep("Verify alert result", () => _alertAction.VerifyAlertResult($"You clicked: Ok"))
                ;
        }
        
        [Fact]
        public void CanVerifyJsPrompt()
        {
            var testCase = new TestCase(_testOutputHelper, "Can verify JS Prompt");
            var inputForAlert = "JS Alert";
            
            testCase
                .ExecuteTestStep("Open Heroku App", () => _selenium.NavigateToUrl(Constants.JavaScriptAlertsUrl))
                .ExecuteTestStep("Click for JS Prompt",() => _alertAction.ClickForJsAlert(AlertType.Prompt))
                .ExecuteTestStep("Verify alert text", () => _selenium.HandleAlert("I am a JS prompt", inputForAlert))
                .ExecuteTestStep("Verify alert result", () => _alertAction.VerifyAlertResult($"You entered: {inputForAlert}"))
                ;
        }


        public void Dispose()
        {
            _selenium.Dispose();
        }
    }
}