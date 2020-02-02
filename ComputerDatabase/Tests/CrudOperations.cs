using System;
using System.Threading.Tasks;
using AutomationFramework;
using ComputerDatabase.Actions;
using Xunit;
using Xunit.Abstractions;

namespace ComputerDatabase.Tests
{
    public class CrudOperations : IDisposable
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly SeleniumInteractions _selenium;
        private readonly CrudActions _crudActions;

        public CrudOperations(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _selenium = new SeleniumInteractions(_testOutputHelper);
            _crudActions = new CrudActions(_selenium);
        }

        [Fact]
        public void CanCreateNewComputer()
        {
            var testCase = new TestCase(_testOutputHelper, "Can create new computer");
            var computer = new Computer
            {
                Company = Company.Apple,
                ComputerName = "Computer name",
                DiscontinuedDate = DateTime.Today,
                IntroducedDate = DateTime.MinValue
            };


            testCase
                .ExecuteTestStep("Open computer database app",
                    () => _selenium.NavigateToUrl(Constants.ComputerDataBaseUrl))
                .ExecuteTestStep("Add a new computer", () => _crudActions.CreateNewComputer(computer))
                ;
        }

        [Fact]
        public async Task CanReadExistingComputer()
        {
            var testCase = new TestCase(_testOutputHelper, "Can read existing computer");
            var computerName = Guid.NewGuid().ToString();

            var computer = new Computer
            {
                Company = Company.Apple,
                ComputerName = computerName,
                DiscontinuedDate = DateTime.Today,
                IntroducedDate = DateTime.MinValue
            };

            await _crudActions.CreateNewComputerApi(computer);

            testCase
                .ExecuteTestStep("Open computer database app",
                    () => _selenium.NavigateToUrl(Constants.ComputerDataBaseUrl))
                .ExecuteTestStep("Read the computer data", () => _crudActions.ReadComputerData(computer))
                ;
        }

        [Fact]
        public async Task CanUpdateExistingComputer()
        {
            var testCase = new TestCase(_testOutputHelper, "Can update existing computer");
            var computerName = Guid.NewGuid().ToString();
            var newComputerName = Guid.NewGuid().ToString();

            var computer = new Computer
            {
                Company = Company.Apple,
                ComputerName = computerName,
                DiscontinuedDate = DateTime.Today,
                IntroducedDate = DateTime.Today
            };

            var computerUpdated = new Computer
            {
                Company = Company.Netronics,
                ComputerName = newComputerName,
                DiscontinuedDate = DateTime.Today.AddDays(1),
                IntroducedDate = DateTime.Today.AddDays(-1)
            };

            await _crudActions.CreateNewComputerApi(computer);

            testCase
                .ExecuteTestStep("Open computer database app",
                    () => _selenium.NavigateToUrl(Constants.ComputerDataBaseUrl))
                .ExecuteTestStep("Update the computer data",
                    () => _crudActions.UpdateComputerData(computerName, computerUpdated))
                .ExecuteTestStep("Read the computer data", () => _crudActions.ReadComputerData(computerUpdated))
                ;
        }

        [Fact]
        public async Task CanDeleteTheComputer()
        {
            var testCase = new TestCase(_testOutputHelper, "Can update existing computer");
            var computerName = Guid.NewGuid().ToString();

            var computer = new Computer
            {
                Company = Company.Apple,
                ComputerName = computerName,
                DiscontinuedDate = DateTime.Today,
                IntroducedDate = DateTime.Today
            };
            
            await _crudActions.CreateNewComputerApi(computer);

            testCase
                .ExecuteTestStep("Open computer database app",
                    () => _selenium.NavigateToUrl(Constants.ComputerDataBaseUrl))
                .ExecuteTestStep("Update the computer data",
                    () => _crudActions.DeleteComputer(computerName))
                ;
        }

        public void Dispose()
        {
            _selenium.Dispose();
        }
    }
}