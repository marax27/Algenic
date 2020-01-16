using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace Algenic.FunctionalTests.Setup
{
    internal static class ContestExtensions
    {
        internal static void CreateContest(this IWebDriver driver, Uri indexUrl, string contestName)
        {
            driver.Navigate().GoToUrl(new Uri(indexUrl, "/Contests"));
            var form = driver.FindElement(By.Id("form-add-group"));
            AuthorizationExtensions.FillForm(form, new Dictionary<string, string>
            {
                {"ContestName", contestName }
            });
        }
    }
}
