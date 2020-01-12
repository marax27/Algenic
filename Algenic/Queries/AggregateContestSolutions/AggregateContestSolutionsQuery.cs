namespace Algenic.Queries.AggregateContestSolutions
{
    public class AggregateContestSolutionsQuery
    {
        public int ContestId { get; }

        private AggregateContestSolutionsQuery(int contestId) => ContestId = contestId;

        public static AggregateContestSolutionsQuery Create(int contestId)
        {
            return new AggregateContestSolutionsQuery(contestId);
        }
    }
}
