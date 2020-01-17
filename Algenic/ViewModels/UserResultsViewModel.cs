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
        public string Username { get; set; }
        public int UserScore { get; set; }
        public int Position { get; set; }
        public IEnumerable<TaskWithTestResults> TaskResults { get; set; }
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
