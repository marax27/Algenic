using System;
using System.Threading.Tasks;
using Algenic.Data;
using Algenic.Data.Models;
using Algenic.Routing;
using Algenic.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Algenic.Areas.Contests.Pages
{
    public class ViewModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        [BindProperty]
        public DisplayContestViewModel Contest { get; set; }

        public ViewModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async System.Threading.Tasks.Task<IActionResult> OnGetAsync(int id)
        {
            var defaultRedirections = new DefaultRedirections(this);

            if (!User.Identity.IsAuthenticated)
                return defaultRedirections.ToLoginPage(HttpContext.Request.Path);

            var contest = await _context.Contests.FindAsync(id);
            Contest = MapToDisplayViewModel(contest);

            var currentUserId = _userManager.GetUserId(User);

            if (currentUserId == Contest.OwnerId)
                return defaultRedirections.ToAccessDeniedPage(HttpContext.Request.Path);
                
            return Page();
        }

        public async Task<IActionResult> OnPostSolveAsync(int taskId)
        {
            return RedirectToPage("View", new { area = "Tasks", id = taskId });
        }

        private DisplayContestViewModel MapToDisplayViewModel(Contest contest)
            => new DisplayContestViewModel
            {
                Name = contest.Name,
                OwnerId = contest.IdentityUser.Id,
                Status = contest.Status,
                NotStarted = contest.Status == Algenic.Data.Models.Contest.ContestState.NotStarted,
                Tasks = contest.Tasks
            };
    }
}