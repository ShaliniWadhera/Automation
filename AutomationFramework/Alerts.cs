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
            var alert = GetAlert();
            var alertPresent = (alert != null);
            
            _testOutputHelper.WriteLine($"    Alert present = {alertPresent}");
            return alertPresent;
        }

        private string GetAlertText()
        {
            var alert = GetAlert();
            var alertText = alert.Text;
            
            _testOutputHelper.WriteLine($"    Alert text = {alertText}");
            return alertText;
        }

        private void AlertAccept()
        {
            var alert = GetAlert();
            alert.Accept();
            
            _testOutputHelper.WriteLine($"    Alert Accepted");
        }

        private IAlert GetAlert()
        {
            return SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent().Invoke(_driver); 
        }
        
        private void AlertSetText(string text)
        {
            var alert = GetAlert();

            alert.SendKeys(text);
            _testOutputHelper.WriteLine($"    Set text to alert {text}");
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

        public string HandleAlert(string alertExpectedText, string inputText = "")
        {
            if (!AlertIsPresent())
            {
                throw new Exception("Cannot find alert");
            }

            var alertText = GetAlertText();
            alertText.ShouldBeGreaterThanOrEqualTo(alertExpectedText,
                $"Alert text did not match the expected result, Actual = `{alertText}` Expected = `{alertExpectedText}`");

            if (inputText != "")
            {
                AlertSetText(inputText);
            }
            
            AlertAccept();

            return alertText;
        }
    }
}