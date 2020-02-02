using System;
using Xunit.Abstractions;

namespace AutomationFramework
{
    public class TestCase
    {
        private readonly ITestOutputHelper _testOutputHelper;
        
        private int _stepNumber = 1;

        public TestCase(ITestOutputHelper testOutputHelper, string testCaseName,
            BrowserType browser = BrowserType.Chrome)
        {
            _testOutputHelper = testOutputHelper;
            _testOutputHelper.WriteLine("***********************************************");
            _testOutputHelper.WriteLine("* " + testCaseName);
            _testOutputHelper.WriteLine("***********************************************");

            _testOutputHelper = testOutputHelper;
           
        }

        public TestCase ExecuteTestStep(string stepDescription, Action action)
        {
            _testOutputHelper.WriteLine($"\n{_stepNumber++} - '{stepDescription}'");
            action();
            return this;
        }
    }
}
