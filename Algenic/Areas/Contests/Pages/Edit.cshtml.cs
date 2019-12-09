using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Algenic.Data.Models;
using Algenic.Data;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

// URL will be (...)/Edit?id=x, if you want (...) /Edit/x, change @page to @page "{id:int}" in Edit.cshtml

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

        public async System.Threading.Tasks.Task OnGetAsync(int id)
        {
            Id = id; 
            TempData.Keep(nameof(Id));
            var contest = await _context.Contests.FindAsync(id);
            ContestName = contest.Name;
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
