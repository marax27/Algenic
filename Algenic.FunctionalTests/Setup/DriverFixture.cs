using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Opera;

namespace Algenic.FunctionalTests.Setup
{
    public class DriverFixture
    {
        public IWebDriver WebDriver { get; }
        public string IndexUrl { get; }

        private readonly IConfiguration _configuration;

        public DriverFixture()
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("testSettings.json")
                .Build();

            WebDriver = CreateWebDriver(_configuration["TestBrowser"]);
            IndexUrl = _configuration["ApplicationIndexUrl"];
        }

        private IWebDriver CreateWebDriver(string browserName)
        {
            switch (browserName)
            {
                case "Firefox":
                    return GetFirefoxDriver();
                case "Chrome":
                    return GetChromeDriver();
                case "Opera":
                    return GetOperaDriver();
                default:
                    throw new UnsupportedBrowserException(browserName);
            }
        }

        private static FirefoxDriver GetFirefoxDriver() => new FirefoxDriver(".");

        private static ChromeDriver GetChromeDriver() => new ChromeDriver(".");

        private static OperaDriver GetOperaDriver() => new OperaDriver(".");
    }
}
