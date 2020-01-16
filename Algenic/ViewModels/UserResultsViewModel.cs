using Algenic.Queries.TaskScore;
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
        public IEnumerable<TaskScoreQueryResult> TaskScores { get; }

        public UserResultsViewModel(string username, IEnumerable<TaskScoreQueryResult> taskScores)
        {
            Username = username;
            TaskScores = taskScores;
            UserScore = taskScores
                .Select(t => t.Score)
                .Sum();
        }
    }
}
