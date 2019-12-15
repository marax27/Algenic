using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Algenic.Commands.CreateContest;
using Algenic.Commons;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Algenic.Data.Models;
using Algenic.Data;
using Algenic.Queries.ContestOwner;
using Algenic.ViewModels;
using Microsoft.AspNetCore.Identity;
using static Algenic.Data.Models.Contest;

namespace Algenic.Areas.Contests.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        private readonly ICommandHandler<CreateContestCommand> _createContestCommandHandler;
        private readonly IQueryHandler<ContestOwnerQuery, ContestOwnerResult> _contestOwnerQueryHandler;

        [BindProperty]
        public string ContestName { get; set; }

        [BindProperty]
        public ICollection<ContestViewModel> Contests { get; set; }

        [BindProperty]
        public bool CanAddContest { get; set; }

        public IndexModel(ApplicationDbContext context, 
            ICommandHandler<CreateContestCommand> createContestCommandHandler,
            IQueryHandler<ContestOwnerQuery, ContestOwnerResult> contestOwnerQueryHandler)
        {
            _context = context;
            _createContestCommandHandler = createContestCommandHandler;
            _contestOwnerQueryHandler = contestOwnerQueryHandler;
        }

        public async System.Threading.Tasks.Task OnGetAsync()
        {
            CanAddContest = User.IsInRole("Admin") || User.IsInRole("Examiner");
            Contests = _context.Contests.Select(MapToViewModel)
                .OrderBy(c => 
                c.Status == ContestState.NotStarted && c.CanEdit ? 1:
                c.Status == ContestState.InProgress && c.CanEdit ? 2:
                c.Status == ContestState.InProgress && c.CanJoin ? 3:
                c.Status == ContestState.NotStarted && c.CanJoin ? 4:
                c.Status == ContestState.Completed && c.CanEdit ? 5: 6)
                .ToList();
        }


        public async Task<IActionResult> OnPostCreateAsync()
        {
            var command = CreateContestCommand.Create(ContestName, User);
            await _createContestCommandHandler.HandleAsync(command);

            ContestName = "";
            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnPostEditAsync(int contestId)
        {
            return RedirectToPage("Edit", new { id = contestId });
        }

        public async Task<IActionResult> OnPostJoinAsync(int contestId)
        {
            return RedirectToPage("View", new { id = contestId });
        }

        public async Task<IActionResult> OnPostDeleteAsync(int contestId)
        {
            var contest = await _context.Contests.FindAsync(contestId);
            _context.Contests.Remove(contest);
            await _context.SaveChangesAsync();
            return RedirectToPage("Index");
        }

        private ContestViewModel MapToViewModel(Contest contest)
        {
            var query = ContestOwnerQuery.Create(contest.Id, User);
            var queryResult = _contestOwnerQueryHandler.HandleAsync(query).Result;
            bool isOwner = queryResult.IsOwner; 

            return new ContestViewModel
            {
                CanEdit = isOwner,
                CanJoin = !isOwner,
                Name = contest.Name,
                Id = contest.Id,
                Author = contest.IdentityUser.UserName,
                Status = contest.Status
            };
        }
    }
}