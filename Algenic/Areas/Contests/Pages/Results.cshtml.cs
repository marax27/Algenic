using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Algenic.Commons;
using Algenic.Data;
using Algenic.Data.Models;
using Algenic.Queries.ContestScoreQuery;
using Algenic.Queries.ContestsUsers;
using Algenic.Queries.NewestSolutions;
using Algenic.Queries.TaskScore;
using Algenic.Queries.TestResults;
using Algenic.Routing;
using Algenic.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Algenic.Areas.Contests.Pages
{
    public class ResultsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IQueryHandler<ContestsUsersQuery, ContestsUsersQueryResult> _contestsUsersQueryHandler;
        private readonly IQueryHandler<TaskScoreQuery, TaskScoreQueryResult> _taskScoreQueryHandler;
        private readonly IQueryHandler<TestResultsQuery, TestResultsQueryResult> _testResultsQueryHandler;
        private readonly IQueryHandler<ContestScoreQuery, ContestScoreQueryResult> _contestScoreQueryHandler;
        private readonly IQueryHandler<NewestSolutionsQuery, NewestSolutionsResult> _newestSolutionsQueryHandler;

        public int ContestId { get; set; }
        [BindProperty]
        public List<UserResultsViewModel> UsersResults { get; set; }

        public ResultsModel(ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            IQueryHandler<ContestsUsersQuery, ContestsUsersQueryResult> contestsUsersQueryHandler,
            IQueryHandler<TaskScoreQuery, TaskScoreQueryResult> taskScoreQueryHandler,
            IQueryHandler<TestResultsQuery, TestResultsQueryResult> testResultsQueryHandler,
            IQueryHandler<ContestScoreQuery, ContestScoreQueryResult> contestScoreQueryHandler,
            IQueryHandler<NewestSolutionsQuery, NewestSolutionsResult> newestSolutionsQueryHandle)
        {
            _context = context;
            _userManager = userManager;
            _contestsUsersQueryHandler = contestsUsersQueryHandler;
            _taskScoreQueryHandler = taskScoreQueryHandler;
            _testResultsQueryHandler = testResultsQueryHandler;
            _contestScoreQueryHandler = contestScoreQueryHandler;
            _newestSolutionsQueryHandler = newestSolutionsQueryHandle;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var defaultRedirections = new DefaultRedirections(this);

            if (!User.Identity.IsAuthenticated)
                return defaultRedirections.ToLoginPage(HttpContext.Request.Path);

            ContestId = id;
            var contestsUsersQuery = ContestsUsersQuery.Create(id);
            var contestsUsers = await _contestsUsersQueryHandler.HandleAsync(contestsUsersQuery);
            var contest = await _context.Contests.FindAsync(id);
            var contestOwnerId = contest.IdentityUser.Id;
            var currentUserId = _userManager.GetUserId(User);

            if (!contestsUsers.UserIds.Append(contestOwnerId).Contains(currentUserId) || contest.Status != Contest.ContestState.Completed)
                return defaultRedirections.ToAccessDeniedPage(HttpContext.Request.Path);

            UsersResults = new List<UserResultsViewModel>();

            IEnumerable<string> usersToIterateOver = contestOwnerId == currentUserId ?
                contestsUsers.UserIds : new[] { currentUserId };

            var ranking = await ContestRanking(contestsUsers.UserIds);

            foreach (var userId in usersToIterateOver)
            {
                var userResults = await AggregateUserResults(userId, ranking);
                UsersResults.Add(userResults);
            }

            return Page();
        }

        private async Task<UserResultsViewModel> AggregateUserResults(string userId, IDictionary<string, int> userRanking)
        {
            var user = await _context.Users.FindAsync(userId);
            var contest = await _context.Contests.FindAsync(ContestId);
            var taskScoreQueries = contest.Tasks.Select(t => TaskScoreQuery.Create(t.Id, user.Id));
            var taskResults = new List<TaskWithTestResults>();
            var taskScores = new List<TaskScoreQueryResult>();

            foreach (var query in taskScoreQueries)
            {
                var taskScore = await _taskScoreQueryHandler.HandleAsync(query);
                var task = await _context.Tasks.FindAsync(query.TaskId);
                var tests = _context.Tests.Where(t => t.TaskId == task.Id);

                var solution = await _context.Solutions
                    .Where(s => s.TaskId == task.Id && s.IdentityUser.Id == userId)
                    .DefaultIfEmpty()
                    .SingleAsync();
                var testResults = new List<TestResultsQueryResult>();

                if (solution != null)
                {
                    foreach (var test in tests)
                    {
                        var testResultsQuery = TestResultsQuery.Create(test.Id, solution.Id);
                        var testResult = await _testResultsQueryHandler.HandleAsync(testResultsQuery);
                        testResults.Add(testResult);
                    }
                }

                taskResults.Add(new TaskWithTestResults(taskScore, testResults));
            }

            return MapToUserResultsViewModel(user.UserName, userRanking[userId], taskResults);
        }

        private async Task<IDictionary<string, int>> ContestRanking(IEnumerable<string> userIds)
        {
            var userRanking = new Dictionary<string, int>();

            foreach (var userId in userIds)
            {
                var contestScoreQuery = ContestScoreQuery.Create(userId, ContestId);
                var contestScore = await _contestScoreQueryHandler.HandleAsync(contestScoreQuery);
                userRanking.Add(userId, contestScore.Score);
            }

            int position = 0;

            userRanking = userRanking
                .OrderByDescending(k => k.Value)
                .Select(v =>
                {
                    ++position;
                    return (v.Key, position);
                })
                .ToDictionary(v => v.Key, v => v.position);

            return userRanking;
        }

        private UserResultsViewModel MapToUserResultsViewModel(string username, int position, IEnumerable<TaskWithTestResults> taskResults)
            => new UserResultsViewModel()
            {
                Username = username,
                Position = position,
                TaskResults = taskResults,
                UserScore = taskResults
                .Select(t => t.TaskScore.Score)
                .Sum(),
            };
    }
}
