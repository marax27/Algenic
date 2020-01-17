using Algenic.Commons;
using Algenic.Commons.DesignByContract;
using Algenic.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Algenic.Queries.ContestsUsers
{
    public class ContestsUsersQuery
    {
        public int ContestId { get; }

        private ContestsUsersQuery(int contestId)
        {
            ContestId = contestId;
        }

        public static ContestsUsersQuery Create(int contestId)
        {
            Fail.If(contestId <= 0);

            return new ContestsUsersQuery(contestId);
        }
    }

    public class ContestsUsersQueryHandler : IQueryHandler<ContestsUsersQuery, ContestsUsersQueryResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public ContestsUsersQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ContestsUsersQueryResult> HandleAsync(ContestsUsersQuery query)
        {
            var contest = await _dbContext.Contests.FindAsync(query.ContestId);

            ISet<string> userIds = contest.Tasks
                .SelectMany(t => t.Solutions.Select(s => s.IdentityUser.Id))
                .ToHashSet();

            return new ContestsUsersQueryResult(userIds);
        }
    }

    public class ContestsUsersQueryResult
    {
        public IEnumerable<string> UserIds { get; }

        public ContestsUsersQueryResult(IEnumerable<string> userIds)
        {
            UserIds = userIds;
        }
    }
}
