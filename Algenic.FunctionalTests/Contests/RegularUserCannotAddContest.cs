using System;
using Algenic.FunctionalTests.Setup;
using FluentAssertions;
using OpenQA.Selenium;
using Xunit;

namespace Algenic.FunctionalTests.Contests
{
    public class RegularUserCannotAddContest : BaseFunctionalTest
    {
        public RegularUserCannotAddContest(DriverFixture driverFixture)
            : base(driverFixture)
        { }

        [Fact]
        public void ContestPage_RegularUser_NoContestAddingForm()
        {
            var testUser = RandomRegularUser.Generate();
            _driver.RegisterUser(_indexUrl, testUser);
            _driver.LoginAs(_indexUrl, testUser);

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
