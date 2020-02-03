using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutomationFramework;
using OpenQA.Selenium;
using Shouldly;

namespace ComputerDatabase.Actions
{
    public enum AlertType
    {
        created,
        updated,
        deleted
    }

    class CrudActions
    {
        private readonly SeleniumInteractions _selenium;

        public CrudActions(SeleniumInteractions selenium)
        {
            _selenium = selenium;
        }

        public void CreateNewComputer(Computer computer)
        {
            var lblAddComputer = By.XPath("//h1[text()='Add a computer']");
            var buttonAddNewComputer = By.Id("add");
            var buttonCreateThisComputer = By.CssSelector(".actions input[type=submit]");

            _selenium.Click(buttonAddNewComputer);
            _selenium.IsElementReady(lblAddComputer).ShouldBeTrue("Add a computer label should be visible");
            FillComputerData(computer);
            _selenium.Click(buttonCreateThisComputer);

            CheckAlert(AlertType.created, computer.ComputerName);
        }

        public void CheckAlert(AlertType alertType, string computerName = "")
        {
            // Talk to team to have delete alert with computer name and remove the hack

            var alertMessage = computerName != ""
                ? $"'Computer {computerName} has been {alertType.ToString()}'"
                : $"'Computer has been {alertType.ToString()}'";

            var alertCreated =
                By.XPath(
                    $"//div[@class='alert-message warning'][contains(.,{alertMessage})]");

            //verify creation
            _selenium.IsElementReady(alertCreated).ShouldBeTrue("Created alert should be visible");
        }

        public void FillComputerData(Computer computer)
        {
            var txtComputerName = By.Id("name");
            var txtIntroducedDate = By.Id("introduced");
            var txtDiscontinuedDate = By.Id("discontinued");
            var dropDownCompany = By.Id("company");

            _selenium.SetValue(txtComputerName, computer.ComputerName);
            _selenium.SetValue(txtIntroducedDate, computer.IntroducedDate.ToString("yyyy-MM-dd"));
            _selenium.SetValue(txtDiscontinuedDate, computer.DiscontinuedDate.ToString("yyyy-MM-dd"));
            _selenium.SelectText(dropDownCompany, computer.Company);
        }

        public async Task CreateNewComputerApi(Computer computer)
        {
            var client = new HttpClient();

            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("name", computer.ComputerName),
                new KeyValuePair<string, string>("introduced", computer.IntroducedDate.ToString("yyyy-MM-dd")),
                new KeyValuePair<string, string>("discontinued", computer.DiscontinuedDate.ToString("yyyy-MM-dd")),
                new KeyValuePair<string, string>("company", "1")
            });

            var response = await client.PostAsync(Constants.ComputerDataBaseUrl, formContent);
            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);
        }

        public void ReadComputerData(Computer computer)
        {
            SearchComputer(computer.ComputerName);

            var tableData =
                _selenium.GetWebElements(By.XPath("//table[contains(@class,'computers')] //tbody //tr //td"));

            tableData[0].Text.ShouldBeEquivalentTo(computer.ComputerName);
            tableData[1].Text.ShouldBeEquivalentTo(computer.IntroducedDate.ToString("dd MMM yyyy"));
            tableData[2].Text.ShouldBeEquivalentTo(computer.DiscontinuedDate.ToString("dd MMM yyyy"));
            tableData[3].Text.ShouldBeEquivalentTo(computer.Company);

            _selenium.WriteLine(helper =>
                helper.WriteLine($"Given data matched for the computer `{computer.ComputerName}`"));
        }

        public void UpdateComputerData(string computerName, Computer computerToUpdate)
        {
            var buttonSaveThisComputer = By.CssSelector(".actions input[type=submit]");

            SearchComputer(computerName);
            OpenComputerDetails(computerName);
            FillComputerData(computerToUpdate);
            _selenium.Click(buttonSaveThisComputer);
            CheckAlert(AlertType.updated, computerToUpdate.ComputerName);
        }

        public void OpenComputerDetails(string computerName)
        {
            var computerNameLink = By.XPath($"//a[text()='{computerName}']");
            _selenium.Click(computerNameLink);
        }

        public void SearchComputer(string computerName)
        {
            var txtSearchBox = By.Id("searchbox");
            var buttonFilterByName = By.Id("searchsubmit");

            _selenium.SetValue(txtSearchBox, computerName);
            _selenium.Click(buttonFilterByName);
        }

        public void DeleteComputer(string computerName)
        {
            var buttonDelete = By.XPath("//input[@value='Delete this computer']");
            var lblEditComputer = By.XPath("//h1[text()='Edit computer']");
           
            SearchComputer(computerName);
            OpenComputerDetails(computerName);
            _selenium.IsElementReady(lblEditComputer).ShouldBeTrue("Edit computer label should be visible");
            _selenium.Click(buttonDelete);
            CheckAlert(AlertType.deleted);
        }
    }

    public class Computer
    {
        public string ComputerName { get; set; }
        public DateTime IntroducedDate { get; set; }
        public DateTime DiscontinuedDate { get; set; }
        public string Company { get; set; }
    }

    public static class Company
    {
        public const string Apple = "Apple Inc.";
        public const string ThinkingMachines = "Thinking Machines";
        public const string Rca = "RCA";
        public const string Netronics = "Netronics";
        public const string TandyCorporation = "Tandy Corporation";
        public const string CommodoreInternational = "Commodore International";
    }
}