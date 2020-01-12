using System.Collections.Generic;

namespace Algenic.ViewModels
{
    public class ContestSolutionAggregate
    {
        public IDictionary<string, UserSolutionAggregate> Users { get; } = new Dictionary<string, UserSolutionAggregate>();
    }

    public class UserSolutionAggregate
    {
        public ISet<int> Tasks { get; } = new HashSet<int>();
    }
}
