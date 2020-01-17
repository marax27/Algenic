using Algenic.Queries.TaskScore;
using Algenic.Queries.TestResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Algenic.ViewModels
{
    public class UserResultsViewModel
    {
        public string Username { get; }
        public int UserScore { get; }
        public int Position { get; }
        public IEnumerable<TaskWithTestResults> TaskResults { get; }

        public UserResultsViewModel(string username, int position, IEnumerable<TaskWithTestResults> taskResults)
        {
            Username = username;
            Position = position;
            TaskResults = taskResults;
            UserScore = taskResults
                .Select(t => t.TaskScore.Score)
                .Sum();
        }
    }

    public class TaskWithTestResults
    {
        public TaskScoreQueryResult TaskScore { get; }
        public IEnumerable<TestResultsQueryResult> TestResults { get; }

        public TaskWithTestResults(TaskScoreQueryResult taskScore, IEnumerable<TestResultsQueryResult> testResults)
        {
            TaskScore = taskScore;
            TestResults = testResults;
        }
    }
}
