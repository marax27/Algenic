using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Algenic.Commons;
using Algenic.Data;
using Algenic.Data.Models;
using Algenic.Queries.ContestsUsers;
using Algenic.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Algenic.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        private readonly IQueryHandler<ContestsUsersQuery, ContestsUsersQueryResult> _contestsUsersQueryHandler;

        [BindProperty]
        public StartingPageViewModel StartingPageViewModel { get; set; }

        public IndexModel(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager,
            IQueryHandler<ContestsUsersQuery, ContestsUsersQueryResult> contestsUsersQueryHandler)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _contestsUsersQueryHandler = contestsUsersQueryHandler;
        }

        public async System.Threading.Tasks.Task OnGetAsync()
        {
            var currentUserId = _userManager.GetUserId(User);
            var contestsData = new List<(int, string)>();

            foreach (var contest in _dbContext.Contests)
            {
                var contestsUsersQuery = ContestsUsersQuery.Create(contest.Id);
                var contestsUsers = await _contestsUsersQueryHandler.HandleAsync(contestsUsersQuery);

                if (contestsUsers.UserIds.Contains(currentUserId) && contest.Status == Contest.ContestState.Completed)
                    contestsData.Add((contest.Id, contest.Name));
            }

            StartingPageViewModel = MapToStartingPageViewModel(contestsData);
        }

        public IActionResult OnPostCheckResults(int contestId)
        {
            return RedirectToPage("Results", new { area = "Contests", id = contestId });
        }

        private StartingPageViewModel MapToStartingPageViewModel(IEnumerable<(int, string)> contests)
            => new StartingPageViewModel()
            {
                Contests = contests
            };
    }
}
