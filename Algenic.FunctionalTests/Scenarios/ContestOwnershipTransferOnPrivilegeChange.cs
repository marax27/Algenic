using System;
using System.Linq;
using Algenic.FunctionalTests.Setup;
using FluentAssertions;
using OpenQA.Selenium;
using TestStack.BDDfy;
using Xunit;

namespace Algenic.FunctionalTests.Scenarios
{
    public class ContestOwnershipTransferOnPrivilegeChange : BaseFunctionalTest
    {
        private UserCredentials TestUser { get; }
        private UserCredentials Admin { get; }
        private readonly string GivenContestName = "Contest Ownership Test: sample contest";

        public ContestOwnershipTransferOnPrivilegeChange(DriverFixture driverFixture) : base(driverFixture)
        {
            TestUser = driverFixture.PredefinedUsers.RegularUser;
            Admin = driverFixture.PredefinedUsers.Admin;
        }

        public void GivenAdminMakesUserAnExaminer()
        {
            _driver.LoginAs(_indexUrl, Admin);
            _driver.Navigate().GoToUrl(new Uri(_indexUrl, "/Admin"));

            var roleChangeButton = GetRoleChangeButton(TestUser.Email);
            if(roleChangeButton.GetAttribute("value").Contains("Grant"))
                roleChangeButton.Click();

            _driver.LogOut(_indexUrl);
        }

        public void AndGivenUserCreatesContest()
        {
            _driver.LoginAs(_indexUrl, TestUser);
            _driver.CreateContest(_indexUrl, GivenContestName);
            _driver.LogOut(_indexUrl);
        }

        public void WhenTheyLoseExaminerRights()
        {
            _driver.LoginAs(_indexUrl, Admin);
            _driver.Navigate().GoToUrl(new Uri(_indexUrl, "/Admin"));
            GetRoleChangeButton(TestUser.Email).Click();
            _driver.LogOut(_indexUrl);
        }

        public void ThenCreatedContestIsTransferredToAdmin()
        {
            _driver.Navigate().GoToUrl(new Uri(_indexUrl, "/Contests"));
            var authorEmail = GetContestAuthor(GivenContestName);
            authorEmail.Should().Be(Admin.Email);
        }

        [Fact]
        public void Scenario()
        {
            this.BDDfy();
        }

        private IWebElement GetRoleChangeButton(string userName)
        {
            var buttonXPath = $"//td[contains(text(), '{userName}')]/..//input[@type='submit']";
            return _driver.FindElements(By.XPath(buttonXPath)).Single();
        }

        private string GetContestAuthor(string givenContestName)
        {
            var authorXPath = $"//td[contains(text(), '{givenContestName}')]/../td[2]";
            return _driver.FindElement(By.XPath(authorXPath)).Text;
        }
    }
}
