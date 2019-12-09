using System;
using System.Collections.Generic;
using System.Linq;
using Algenic.FunctionalTests.Setup;
using FluentAssertions;
using OpenQA.Selenium;
using Xunit;

namespace Algenic.FunctionalTests
{
    public class AdminPanelAccess : BaseFunctionalTest
    {
        public AdminPanelAccess(DriverFixture driverFixture)
            : base(driverFixture) { }

        [Fact]
        public void AccessAdminPanel_RegularUser_AccessDenied()
        {
            var guid = Guid.NewGuid();
            var email = $"{guid}@example.com";
            var password = "123456";
            var testUser = new UserCredentials {Email = email, Password = password};

            _driver.RegisterUser(_indexUrl, testUser);
            _driver.LoginAs(_indexUrl, testUser);
            _driver.Navigate().GoToUrl(new Uri(_indexUrl, "/Admin"));

            _driver.PageSource.Should().Contain("Access denied");
        }
    }
}
