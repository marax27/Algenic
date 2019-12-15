using System.Linq;
using System.Threading.Tasks;
using Algenic.Commons;
using Algenic.Data;
using Algenic.Data.Models;

namespace Algenic.Queries.CalculateScore
{
    public class CalculateScoreQueryHandler : IQueryHandler<CalculateScoreQuery, CalculateScoreResult>
    {
        private ApplicationDbContext _dbContext;

        public CalculateScoreQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CalculateScoreResult> HandleAsync(CalculateScoreQuery query)
        {
            var solution = await _dbContext.Solutions.FindAsync(query.SolutionId);
            var percentagePassed = await GetPassedTestPercentage(solution);

            var rules = solution.Task.ScorePolicy.ScoreRules
                .OrderBy(sr => sr.Threshold)
                .ToList();

            int score = 0;
            for (int i = 0; i != rules.Count - 1; ++i)
            {
                var rule = rules[i];
                if (percentagePassed < rule.Threshold)
                    break;
                score = rule.Score;
            }

            return new CalculateScoreResult(score);
        }

        private async Task<decimal> GetPassedTestPercentage(Solution solution)
        {
            var passedTestCount = solution.CompilationResults
                .Count(cr => cr.ExecutionSuccessful && cr.Output == cr.Test.ExpectedOutput);

            var allTestCount = solution.Task.Tests.Count;

            return new decimal(passedTestCount) / new decimal(allTestCount);
        }
    }
}
