using System;
using OpenQA.Selenium;
using Xunit;

namespace Algenic.FunctionalTests.Setup
{
    public abstract class BaseFunctionalTest : IDisposable, IClassFixture<DriverFixture>
    {
        protected readonly IWebDriver _driver;
        protected readonly string _indexUrl;

        public BaseFunctionalTest(DriverFixture driverFixture)
        {
            _driver = driverFixture.WebDriver;
            _indexUrl = driverFixture.IndexUrl;
        }

        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}
