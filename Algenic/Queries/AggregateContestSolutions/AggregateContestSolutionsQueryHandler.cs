using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Algenic.Commons;
using Algenic.Data;
using Algenic.Data.Models;
using Algenic.Queries.NewestSolutions;
using Algenic.ViewModels;

namespace Algenic.Queries.AggregateContestSolutions
{
    public class AggregateContestSolutionsQueryHandler : IQueryHandler<AggregateContestSolutionsQuery, AggregateContestSolutionsResult>
    {
        private ApplicationDbContext _dbContext;
        private IQueryHandler<NewestSolutionsQuery, NewestSolutionsResult> _newestSolutionsQueryHandler;

        public AggregateContestSolutionsQueryHandler(ApplicationDbContext dbContext,
            IQueryHandler<NewestSolutionsQuery, NewestSolutionsResult> newestSolutionsQueryHandler)
        {
            _dbContext = dbContext;
            _newestSolutionsQueryHandler = newestSolutionsQueryHandler;
        }

        public async Task<AggregateContestSolutionsResult> HandleAsync(AggregateContestSolutionsQuery query)
        {
            var solutions = await GetSolutions(query.ContestId);

            var aggregate = new ContestSolutionAggregate();
            foreach (var solution in solutions)
            {
                var userId = solution.IdentityUser.Id;
                var userName = solution.IdentityUser.UserName;
                if (!aggregate.Users.ContainsKey(userId))
                {
                    aggregate.Users[userId] = new UserSolutionAggregate(userName);
                }

                var task = solution.Task;
                aggregate.Users[userId].Tasks.Add(new TaskDto(task.Id, task.Name));
            }

            return new AggregateContestSolutionsResult(aggregate);
        }

        private async Task<IEnumerable<Solution>> GetSolutions(int contestId)
        {
            var newestSolutionsQuery = NewestSolutionsQuery.Create(contestId);
            var result = await _newestSolutionsQueryHandler.HandleAsync(newestSolutionsQuery);
            var solutionIds = result.SolutionIds;

            return solutionIds.Select(id => _dbContext.Solutions.Find(id));
        }
    }
}
