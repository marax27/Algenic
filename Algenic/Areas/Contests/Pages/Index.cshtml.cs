using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Algenic.Commands.CreateContest;
using Algenic.Commons;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Algenic.Data.Models;
using Algenic.Data;
using Microsoft.AspNetCore.Identity;
using static Algenic.Data.Models.Contest;

namespace Algenic.Areas.Contests.Pages
{
    public class ContestViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public bool CanJoin { get; set; }
        public bool CanEdit { get; set; }
        public ContestState Status { get; set; }
    }

    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        private readonly ICommandHandler<CreateContestCommand> _createContestCommandHandler;

        [BindProperty]
        public string ContestName { get; set; }

        [BindProperty]
        public ICollection<ContestViewModel> Contests { get; set; }

        [BindProperty]
        public bool CanAddContest { get; set; }

        public IndexModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _createContestCommandHandler = new CreateContestCommandHandler(context, userManager);
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
            _createContestCommandHandler.Handle(command);

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

        private ContestViewModel MapToViewModel(Contest contest)
        {
            bool isOwner = IsCurrentUsersContest(contest.Id);
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

        private bool IsCurrentUsersContest(int contestId)
        {
            var contestsUserId = _context.Contests.Single(c => c.Id == contestId)
                .IdentityUser.Id;

            return contestsUserId == _userManager.GetUserId(User);
        }
    }
}