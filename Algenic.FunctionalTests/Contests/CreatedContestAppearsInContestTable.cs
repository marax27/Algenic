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
            CreateContest(givenContestName);
            AssertContestPresence(givenContestName);
        }

        private void Login()
            => _driver.LoginAs(_indexUrl, Examiner);

        private void CreateContest(string contestName)
            => _driver.CreateContest(_indexUrl, contestName);

        private void AssertContestPresence(string phrase)
        {
            var contestTable = _driver.FindElement(By.Id("contest-table"));
            var match = contestTable.FindElement(By.XPath($"//tr//td[contains(text(), \"{phrase}\")]"));
            match.Should().NotBeNull();
        }
    }
}
