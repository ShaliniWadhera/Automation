using System;
using AutomationFramework;
using Xunit;
using Xunit.Abstractions;

namespace ComputerDatabase
{
    public class UnitTest1
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private SeleniumInteractions _selenium;

        public UnitTest1(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void CanVerifyJsConfirm()
        {
            _selenium = new SeleniumInteractions(_testOutputHelper);
            var testCase = new TestCase(_testOutputHelper, "Can verify JS Confirm");

            testCase
                .ExecuteTestStep("Open Heroku App", () => _selenium.NavigateToUrl(Constants.ComputerDataBaseUrl))
                ;
        }
    }
}
