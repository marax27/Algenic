using System;

namespace Algenic.FunctionalTests.Setup
{
    public class UnsupportedBrowserException : Exception
    {
        public UnsupportedBrowserException(string browserName)
            : base($"Unsupported browser name: {browserName}.")
        {

        }
    }
}
