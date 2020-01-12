using Algenic.ViewModels;

namespace Algenic.Queries.AggregateContestSolutions
{
    public class AggregateContestSolutionsResult
    {
        public ContestSolutionAggregate Aggregate { get; }

        public AggregateContestSolutionsResult(ContestSolutionAggregate aggregate)
        {
            Aggregate = aggregate;
        }
    }
}
