using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace Algenic.FunctionalTests.Setup
{
    internal static class AuthorizationExtensions
    {
        internal static void LoginAs(this IWebDriver driver, Uri baseUrl, UserCredentials user)
        {
            var loginUrl = new Uri(baseUrl, "/Identity/Account/Login");
            driver.Navigate().GoToUrl(loginUrl);

            var loginForm = driver.FindElements(By.Id("account")).Single();
            FillForm(loginForm, new Dictionary<string, string>
            {
                { "Input_Email", user.Email },
                { "Input_Password", user.Password }
            });
        }

        internal static void LogOut(this IWebDriver driver, Uri baseUrl)
        {
            var logoutUrl = new Uri(baseUrl, "/Identity/Account/Logout");
            driver.Navigate().GoToUrl(logoutUrl);
        }

        internal static void FillForm(IWebElement formElement, IDictionary<string, string> fieldValues)
        {
            foreach (var pair in fieldValues)
            {
                var textBox = formElement.FindElement(By.Id(pair.Key));
                textBox.Clear();
                textBox.SendKeys(pair.Value);
            }

            formElement.Submit();
        }
    }
}
