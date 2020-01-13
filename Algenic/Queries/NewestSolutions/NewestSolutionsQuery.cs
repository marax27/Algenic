using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Algenic.Commons;
using Algenic.Commons.DesignByContract;
using Algenic.Data;
using Algenic.Data.Models;

namespace Algenic.Queries.NewestSolutions
{
    public class NewestSolutionsQuery
    {
        public int ContestId { get; }

        private NewestSolutionsQuery(int contestId) => ContestId = contestId;

        public static NewestSolutionsQuery Create(int contestId)
        {
            Fail.If(contestId <= 0, "Contest ID must be a positive number.");

            return new NewestSolutionsQuery(contestId);
        }
    }

    public class NewestSolutionsResult
    {
        public IEnumerable<int> SolutionIds { get; }

        public NewestSolutionsResult(IEnumerable<int> solutionIds)
            => SolutionIds = solutionIds;
    }

    public class NewestSolutionsQueryHandler : IQueryHandler<NewestSolutionsQuery, NewestSolutionsResult>
    {
        private ApplicationDbContext _dbContext;

        public NewestSolutionsQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<NewestSolutionsResult> HandleAsync(NewestSolutionsQuery query)
        {
            var solutions = _dbContext.Solutions
                .Where(s => s.Task.ContestId == query.ContestId).ToList();

            var uniqueUserIds = solutions.Select(s => s.IdentityUser.Id).Distinct().ToList();
            var uniqueTaskIds = solutions.Select(s => s.TaskId).Distinct().ToList();

            var resultIds = new List<int>();
            foreach (var userId in uniqueUserIds)
            {
                foreach (var taskId in uniqueTaskIds)
                {
                    var matchingSolutions = solutions
                        .Where(s => s.IdentityUser.Id == userId && s.TaskId == taskId)
                        .ToList();
                    var solutionId = TryGetLatestSolutionIdFrom(matchingSolutions);

                    if(solutionId.HasValue)
                        resultIds.Add(solutionId.Value);
                }
            }

            return new NewestSolutionsResult(resultIds);
        }

        private int? TryGetLatestSolutionIdFrom(ICollection<Solution> solutions)
            => solutions
                .OrderByDescending(s => s.TimeStamp)
                .FirstOrDefault()
                ?.Id;
    }
}
