using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Algenic.Commons;
using Algenic.Data;
using Algenic.Data.Models;
using Algenic.Queries.ContestsUsers;
using Algenic.Queries.TaskScore;
using Algenic.Queries.TestResults;
using Algenic.Routing;
using Algenic.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Algenic.Areas.Contests.Pages
{
    public class ResultsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IQueryHandler<ContestsUsersQuery, ContestsUsersQueryResult> _contestsUsersQueryHandler;
        private readonly IQueryHandler<TaskScoreQuery, TaskScoreQueryResult> _taskScoreQueryHandler;
        private readonly IQueryHandler<TestResultsQuery, TestResultsResult> _testResultsQueryHandler;

        public int ContestId { get; set; }
        [BindProperty]
        public List<UserResultsViewModel> UsersResults { get; set; }

        public ResultsModel(ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            IQueryHandler<ContestsUsersQuery, ContestsUsersQueryResult> contestsUsersQueryHandler,
            IQueryHandler<TaskScoreQuery, TaskScoreQueryResult> taskScoreQueryHandler,
            IQueryHandler<TestResultsQuery, TestResultsResult> testResultsQueryHandler)
        {
            _context = context;
            _userManager = userManager;
            _contestsUsersQueryHandler = contestsUsersQueryHandler;
            _taskScoreQueryHandler = taskScoreQueryHandler;
            _testResultsQueryHandler = testResultsQueryHandler;
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

            foreach (var userId in usersToIterateOver)
            {
                var userResults = await AggregateUserResults(userId);
                UsersResults.Add(userResults);
            }

            return Page();
        }

        private async Task<UserResultsViewModel> AggregateUserResults(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            var taskScoreQueries = _context.Tasks.Select(t => TaskScoreQuery.Create(t.Id, user.Id));
            var taskScores = new List<TaskScoreQueryResult>();

            foreach (var query in taskScoreQueries)
            {
                var taskScore = await _taskScoreQueryHandler.HandleAsync(query);
                taskScores.Add(taskScore);
            }

            return new UserResultsViewModel(user.UserName, taskScores);
        }
    }
}
