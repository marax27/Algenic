using System.Collections.Generic;

namespace Algenic.Commons.DesignByContract
{
    public static class Fail
    {
        public static void If(bool condition, string message = "Fail.If failed")
        {
            if(condition)
                throw new DesignByContractException(message);
        }

        public static void IfNull<T>(T obj, string message = "Fail.IfNull failed")
        {
            if(obj == null)
                throw new DesignByContractException(message);
        }

        public static void IfNullOrEmpty<T>(ICollection<T> collection, string message = "Fail.IfNullOrEmpty failed")
        {
            if(collection == null || collection.Count == 0)
                throw new DesignByContractException(message);
        }
    }
}
