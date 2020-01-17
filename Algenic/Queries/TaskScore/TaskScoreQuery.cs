using Algenic.Commons;
using Algenic.Commons.DesignByContract;
using Algenic.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Algenic.Queries.TaskScore
{
    public class TaskScoreQuery
    {
        public int TaskId { get; }
        public string UserId { get; }

        private TaskScoreQuery(int taskId, string userId)
        {
            TaskId = taskId;
            UserId = userId;
        }

        public static TaskScoreQuery Create(int taskId, string userId)
        {
            Fail.If(taskId <= 0);
            Fail.IfNullOrEmpty(userId);

            return new TaskScoreQuery(taskId, userId);
        }
    }

    public class TaskScoreQueryHandler : IQueryHandler<TaskScoreQuery, TaskScoreQueryResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public TaskScoreQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TaskScoreQueryResult> HandleAsync(TaskScoreQuery query)
        {
            var task = await _dbContext.Tasks.FindAsync(query.TaskId);
            var usersScore = task.Solutions
                .Where(s => s.IdentityUser.Id == query.UserId)
                .SingleOrDefault()
                ?.PointCount;

            usersScore = usersScore ?? 0;

            return new TaskScoreQueryResult(task.Name, Convert.ToInt32(usersScore));
        }
    }

    public class TaskScoreQueryResult
    {
        public string TaskName { get; }
        public int Score { get; }

        public TaskScoreQueryResult(string taskName, int score)
        {
            TaskName = taskName;
            Score = score;
        }
    }
}
