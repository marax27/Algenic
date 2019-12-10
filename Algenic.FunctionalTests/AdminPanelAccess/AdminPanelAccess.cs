using System;
using System.Collections.Generic;
using System.Linq;
using Algenic.FunctionalTests.Setup;
using FluentAssertions;
using OpenQA.Selenium;
using Xunit;

namespace Algenic.FunctionalTests.AdminPanelAccess
{
    public class ByRegularUser : BaseFunctionalTest
    {
        private UserCredentials RegularUser { get; }

        public ByRegularUser(DriverFixture driverFixture)
            : base(driverFixture)
        {
            RegularUser = driverFixture.PredefinedUsers.RegularUser;
        }

        [Fact]
        public void AccessAdminPanel_RegularUser_AccessDenied()
        {
            _driver.LoginAs(_indexUrl, RegularUser);

            _driver.Navigate().GoToUrl(new Uri(_indexUrl, "/Admin"));

            _driver.PageSource.Should().Contain("Access denied");
        }
    }

    public class ByExaminer : BaseFunctionalTest
    {
        private UserCredentials Examiner { get; }

        public ByExaminer(DriverFixture driverFixture)
            : base(driverFixture)
        {
            Examiner = driverFixture.PredefinedUsers.Examiner;
        }

        [Fact]
        public void AccessAdminPanel_Examiner_AccessDenied()
        {
            _driver.LoginAs(_indexUrl, Examiner);

            _driver.Navigate().GoToUrl(new Uri(_indexUrl, "/Admin"));

            _driver.PageSource.Should().Contain("Access denied");
        }
    }

    public class ByAdmin : BaseFunctionalTest
    {
        private UserCredentials Admin { get; }

        public ByAdmin(DriverFixture driverFixture)
            : base(driverFixture)
        {
            Admin = driverFixture.PredefinedUsers.Admin;
        }

        [Fact]
        public void AccessAdminPanel_Admin_AccessGranted()
        {
            _driver.LoginAs(_indexUrl, Admin);

            _driver.Navigate().GoToUrl(new Uri(_indexUrl, "/Admin"));

            _driver.PageSource.Should().NotContain("Access denied");
        }
    }
}
