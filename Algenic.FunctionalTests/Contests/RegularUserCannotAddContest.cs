using System;
using Algenic.FunctionalTests.Setup;
using FluentAssertions;
using OpenQA.Selenium;
using Xunit;

namespace Algenic.FunctionalTests.Contests
{
    public class RegularUserCannotAddContest : BaseFunctionalTest
    {
        public UserCredentials RegularUser { get; }

        public RegularUserCannotAddContest(DriverFixture driverFixture)
            : base(driverFixture)
        {
            RegularUser = driverFixture.PredefinedUsers.RegularUser;
        }

        [Fact]
        public void ContestPage_RegularUser_NoContestAddingForm()
        {
            _driver.LoginAs(_indexUrl, RegularUser);

            GoToContestPage();
            var formsOnPage = _driver.FindElements(By.Id("form-add-group"));

            formsOnPage.Should().BeNullOrEmpty();
        }

        private void GoToContestPage()
        {
            _driver.Navigate().GoToUrl(new Uri(_indexUrl, "/Contests"));
        }
    }
}
