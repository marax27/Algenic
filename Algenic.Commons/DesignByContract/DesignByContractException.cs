using System;

namespace Algenic.Commons.DesignByContract
{
    public class DesignByContractException : Exception
    {
        public DesignByContractException(string message)
            : base(message) { }
    }
}
