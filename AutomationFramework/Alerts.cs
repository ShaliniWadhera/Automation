using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Shouldly;

namespace AutomationFramework
{
    public partial class SeleniumInteractions
    {
        private bool AlertIsPresent()
        {
            IAlert alert = ExpectedConditions.AlertIsPresent().Invoke(_driver);
            var alertPresent = (alert != null);
            _testOutputHelper.WriteLine($"    Alert present = {alertPresent}");
            return alertPresent;
        }

        private string GetAlertText()
        {
            IAlert alert = ExpectedConditions.AlertIsPresent().Invoke(_driver);

            var alertText = alert.Text;
            _testOutputHelper.WriteLine($"    Alert text = {alertText}");
            return alertText;
        }

        private void AlertAccept()
        {
            IAlert alert = ExpectedConditions.AlertIsPresent().Invoke(_driver);
            alert.Accept();
            _testOutputHelper.WriteLine($"    Alert Accepted");
        }

        public string HandleAlert()
        {
            if (!AlertIsPresent())
            {
                throw new Exception("Cannot find alert");
            }

            var alertText = GetAlertText();
            AlertAccept();

            return alertText;
        }

        public string HandleAlert(string alertExpectedText)
        {
            if (!AlertIsPresent())
            {
                throw new Exception("Cannot find alert");
            }

            var alertText = GetAlertText();
            alertText.ShouldBeGreaterThanOrEqualTo(alertExpectedText,
                $"Alert text did not match the expected result, Actual = `{alertText}` Expected = `{alertExpectedText}`");

            AlertAccept();

            return alertText;
        }
    }
}