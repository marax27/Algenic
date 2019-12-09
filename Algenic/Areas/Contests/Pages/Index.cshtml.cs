using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Algenic.Data.Models;
using Algenic.Data;
using Microsoft.AspNetCore.Identity;

namespace Algenic.Areas.Contests.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        [BindProperty]
        public string ContestName { get; set; }
        [BindProperty]
        public IEnumerable<Contest> ActiveContests { get; set; }

        public IndexModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async System.Threading.Tasks.Task OnGetAsync()
        {
            ActiveContests = _context.Contests.Where(c => c.Status == Contest.ContestState.InProgress)
                .ToList();
        }


        public async Task<IActionResult> OnPostCreateAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var contest = new Contest()
            {
                Name = ContestName,
                IdentityUser = user,
                Status = Contest.ContestState.NotStarted
            };

            await _context.Contests.AddAsync(contest);
            await _context.SaveChangesAsync();

            ContestName = "";

            return Page();
        }

        public async Task<IActionResult> OnPostEditAsync(int contestId)
        {
            return RedirectToPage("Edit", new { id = contestId });
        }

        public bool IsCurrentUsersContest(int contestId)
        {
            var contestsUserId = _context.Contests.Where(c => c.Id == contestId)
                .Single()
                .IdentityUser.Id;

            var userId = _userManager.GetUserId(User);

            return contestsUserId == userId;
        }
    }
}
