using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Algenic.Data;
using Algenic.Data.Models;
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

        public async System.Threading.Tasks.Task OnGetAsync(int id)
        {
            Contest = await _context.Contests.FindAsync(id);
        }

        public async Task<IActionResult> OnPostSolveAsync(int taskId)
        {
            return null;
        }
    }
}