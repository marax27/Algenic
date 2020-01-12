using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Algenic.Data;
using Algenic.Data.Models;
using Algenic.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Algenic.Areas.Pages.Tasks
{
    public class ViewModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        [BindProperty]
        public Data.Models.Task Task { get; set; }

        [BindProperty]
        public IFormFile SourceCodeFile { get; set; }

        public ViewModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var defaultRedirections = new DefaultRedirections(this);

            if (!User.Identity.IsAuthenticated)
                return defaultRedirections.ToLoginPage(HttpContext.Request.Path);

            Task = await _context.Tasks.FindAsync(id);

            var taskCreatorId = Task.Contest.IdentityUser.Id;
            var currentUserId = _userManager.GetUserId(User);

            if (currentUserId == taskCreatorId || Task.Contest.Status == Contest.ContestState.NotStarted)
                return defaultRedirections.ToAccessDeniedPage(HttpContext.Request.Path);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            throw new NotImplementedException();
        }
    }
}
