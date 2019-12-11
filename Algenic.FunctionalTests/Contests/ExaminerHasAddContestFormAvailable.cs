using System;
using Algenic.FunctionalTests.Setup;
using FluentAssertions;
using OpenQA.Selenium;
using Xunit;

namespace Algenic.FunctionalTests.Contests
{
    public class ExaminerHasAddContestFormAvailable : BaseFunctionalTest
    {
        private UserCredentials ExaminerCredentials { get; }

        public ExaminerHasAddContestFormAvailable(DriverFixture driverFixture)
            : base(driverFixture)
        {
            ExaminerCredentials = driverFixture.PredefinedUsers.Examiner;
        }

        [Fact]
        public void ContestPage_Examiner_AddContestFormIsPresent()
        {
            var examiner = ExaminerCredentials;
            _driver.LoginAs(_indexUrl, examiner);

            GoToContestPage();
            var formsOnPage = _driver.FindElements(By.Id("form-add-group"));

            formsOnPage.Should().HaveCount(1);
        }

        private void GoToContestPage()
        {
            _driver.Navigate().GoToUrl(new Uri(_indexUrl, "/Contests"));
        }
    }
}
