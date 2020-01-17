using Algenic.Commons;
using Algenic.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Algenic.Queries.ContestScoreQuery
{
    public class ContestScoreQuery
    {
        public string UserId { get; }
        public int ContestId { get; }

        private ContestScoreQuery(string userId, int contestId)
        {
            UserId = userId;
            ContestId = contestId;
        }

        public static ContestScoreQuery Create(string userId, int contestId)
        {
            return new ContestScoreQuery(userId, contestId);
        }
    }

    public class ContestScoreQueryHandler : IQueryHandler<ContestScoreQuery, ContestScoreQueryResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public ContestScoreQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ContestScoreQueryResult> HandleAsync(ContestScoreQuery query)
        {
            var contest = await _dbContext.Contests.FindAsync(query.ContestId);

            var score = contest.Tasks.SelectMany(t =>
                t.Solutions
                    .Where(s => s.IdentityUser.Id == query.UserId)
                    ?.Select(s => s.PointCount)
            ).Sum();

            return new ContestScoreQueryResult(query.UserId, query.ContestId, Convert.ToInt32(score));
        }
    }

    public class ContestScoreQueryResult
    {
        public string UserId { get; }
        public int ContestId { get; }
        public int Score { get; }

        public ContestScoreQueryResult(string userId, int contestId, int score)
        {
            UserId = userId;
            ContestId = contestId;
            Score = score;
        }
    }
}
