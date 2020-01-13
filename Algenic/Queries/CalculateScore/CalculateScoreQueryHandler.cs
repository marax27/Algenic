using System;
using System.Linq;
using System.Threading.Tasks;
using Algenic.Commons;
using Algenic.Commons.DesignByContract;
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

            if (solution.Task.Tests.Count != solution.CompilationResults.Count + solution.Logs.Count)
                throw new ArgumentException("Not all tests have been performed");

            var percentagePassed = await GetPassedTestPercentage(solution);

            return new CalculateScoreResult(GetScore(percentagePassed, solution.Task.ScorePolicy));
        }

        public static int GetScore(decimal percentagePassed, ScorePolicy scorePolicy)
        {
            Fail.IfNull(scorePolicy);
            Fail.IfNullOrEmpty(scorePolicy.ScoreRules);

            var rules = scorePolicy.ScoreRules
                .OrderBy(sr => sr.Threshold)
                .ToList();

            var score = 0;
            foreach (var rule in rules)
            {
                if (percentagePassed < rule.Threshold)
                    break;
                score = rule.Score;
            }
            return score;
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
