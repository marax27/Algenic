using System.Linq;
using System.Threading.Tasks;
using Algenic.Commons;
using Algenic.Data;
using Algenic.ViewModels;

namespace Algenic.Queries.AggregateContestSolutions
{
    public class AggregateContestSolutionsQueryHandler : IQueryHandler<AggregateContestSolutionsQuery, AggregateContestSolutionsResult>
    {
        private ApplicationDbContext _dbContext;

        public AggregateContestSolutionsQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AggregateContestSolutionsResult> HandleAsync(AggregateContestSolutionsQuery query)
        {
            var contest = await _dbContext.Contests.FindAsync(query.ContestId);
            var solutions = _dbContext.Solutions
                .Where(s => s.Task.Contest == contest)
                .ToHashSet();

            var aggregate = new ContestSolutionAggregate();
            foreach (var solution in solutions)
            {
                var userId = solution.IdentityUser.Id;
                if (!aggregate.Users.ContainsKey(userId))
                {
                    aggregate.Users[userId] = new UserSolutionAggregate();
                }

                aggregate.Users[userId].Tasks.Add(solution.TaskId);
            }

            return new AggregateContestSolutionsResult(aggregate);
        }
    }
}
