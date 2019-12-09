using System;

namespace Algenic.FunctionalTests.Setup
{
    public static class RandomRegularUser
    {
        public static UserCredentials Generate()
        {
            var guid = Guid.NewGuid();
            var email = $"{guid}@example.com";
            var password = "123456";
            return new UserCredentials { Email = email, Password = password };
        }
    }
}
