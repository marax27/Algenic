using System;
using System.Collections.Generic;
using System.Linq;
using Algenic.FunctionalTests.Setup;
using Xunit;
using FluentAssertions;
using OpenQA.Selenium;

namespace Algenic.FunctionalTests.Contests
{
    public class CreatedContestAppearsInContestTable : BaseFunctionalTest
    {
        public UserCredentials Examiner { get; }

        public CreatedContestAppearsInContestTable(DriverFixture driverFixture)
            : base(driverFixture)
        {
            Examiner = driverFixture.PredefinedUsers.Examiner;
        }

        [Fact]
        public void ContestPage_Examiner_ContestAppearsAfterCreation()
        {
            var givenContestName = Guid.NewGuid().ToString();

            Login();
            GoToContestPage();
            CreateContest(givenContestName);
            AssertContestPresence(givenContestName);
        }

        private void Login()
            => _driver.LoginAs(_indexUrl, Examiner);

        private void GoToContestPage()
            => _driver.Navigate().GoToUrl(new Uri(_indexUrl, "/Contests"));

        private void CreateContest(string contestName)
        {
            var form = _driver.FindElement(By.Id("form-add-group"));
            AuthorizationExtensions.FillForm(form, new Dictionary<string, string>
            {
                { "ContestName", contestName }
            });
        }

        private void AssertContestPresence(string phrase)
        {
            var contestTable = _driver.FindElement(By.Id("contest-table"));
            var match = contestTable.FindElement(By.XPath($"//tr//td[contains(text(), \"{phrase}\")]"));
            match.Should().NotBeNull();
        }
    }
}
