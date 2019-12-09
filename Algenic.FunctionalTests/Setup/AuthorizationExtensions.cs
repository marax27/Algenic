using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        internal static void RegisterUser(this IWebDriver driver, Uri baseUrl, UserCredentials user)
        {
            var registerUrl = new Uri(baseUrl, "/Identity/Account/Register");
            driver.Navigate().GoToUrl(registerUrl);

            var registerForm = driver.FindElements(By.TagName("form")).Single();
            FillForm(registerForm, new Dictionary<string, string>
            {
                { "Input_Email", user.Email },
                { "Input_Password", user.Password },
                { "Input_ConfirmPassword", user.Password }
            });
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
