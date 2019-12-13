using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Algenic.Data;
using Algenic.Data.Models;
using Algenic.Routing;
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
        public Contest Contest { get; set; }

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

            Contest = await _context.Contests.FindAsync(id);

            var contestOwnerId = Contest.IdentityUser.Id;
            var currentUserId = _userManager.GetUserId(User);

            if (currentUserId == contestOwnerId)
                return defaultRedirections.ToAccessDeniedPage(HttpContext.Request.Path);

            return Page();
        }

        public async Task<IActionResult> OnPostSolveAsync(int taskId)
        {
            return null;
        }
    }
}