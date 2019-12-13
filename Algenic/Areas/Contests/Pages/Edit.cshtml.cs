using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Algenic.Data.Models;
using Algenic.Data;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Algenic.Routing;

namespace Algenic.Areas.Contests.Pages
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        [TempData]
        public int Id { get; set; }
        [BindProperty]
        public string ContestName { get; set; }
        [BindProperty]
        public IEnumerable<Algenic.Data.Models.Task> ContestTasks { get; set; }

        public EditModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
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
            var contestOwnerId = contest.IdentityUser.Id;
            var currentUserId = _userManager.GetUserId(User);

            if (currentUserId != contestOwnerId)
                return defaultRedirections.ToAccessDeniedPage(HttpContext.Request.Path);

            Id = id; 
            TempData.Keep(nameof(Id));
            ContestName = contest.Name;

            return Page();
        }

        public async Task<IActionResult> OnPostSaveAsync()
        {
            var contest = await _context.Contests.FindAsync(Id);
            contest.Name = ContestName;
            await _context.SaveChangesAsync();

            return RedirectToPage(Id);
        }
    }
}
