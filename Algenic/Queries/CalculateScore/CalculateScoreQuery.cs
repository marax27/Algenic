using Algenic.Commons.DesignByContract;

namespace Algenic.Queries.CalculateScore
{
    public class CalculateScoreQuery
    {
        public int SolutionId { get; }

        private CalculateScoreQuery(int solutionId)
        {
            SolutionId = solutionId;
        }

        public static CalculateScoreQuery Create(int solutionId)
        {
            return new CalculateScoreQuery(solutionId);
        }
    }
}
