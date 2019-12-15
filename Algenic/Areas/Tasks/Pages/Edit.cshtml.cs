using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Algenic.Data;
using Algenic.Data.Models;
using Algenic.Routing;
using Algenic.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Algenic.Areas.Tasks.Pages
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        [TempData]
        public int TaskId { get; set; }
        [BindProperty]
        public EditTaskViewModel FormTask { get; set; }
        [BindProperty]
        public AddTestViewModel FormTest { get; set; }

        public EditModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var defaultRedirections = new DefaultRedirections(this);

            if (!User.Identity.IsAuthenticated)
                return defaultRedirections.ToLoginPage(HttpContext.Request.Path);

            var task = await _context.Tasks.FindAsync(id);
            var contestOwnerId = task.Contest.IdentityUser.Id;
            var currentUserId = _userManager.GetUserId(User);

            if (currentUserId != contestOwnerId)
                return defaultRedirections.ToAccessDeniedPage(HttpContext.Request.Path);

            FormTask = MapToEditTaskViewModel(task);
            TaskId = id;
            TempData.Keep(nameof(TaskId));

            return Page();
        }

        public async Task<IActionResult> OnPostChangeAsync()
        {
            var task = await _context.Tasks.FindAsync(TaskId);
            task.Name = FormTask.Name;
            task.Description = FormTask.Description;
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostAddTestAsync()
        {
            var test = new Test
            {
                Name = FormTest.Name,
                Input = FormTest.Input,
                ExpectedOutput = FormTest.ExpectedOutput
            };

            var task = await _context.Tasks.FindAsync(TaskId);
            task.Tests.Add(test);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostRemoveTestAsync(int testId)
        {
            var task = await _context.Tasks.FindAsync(TaskId);
            var test = await _context.Tests.FindAsync(testId);
            task.Tests.Remove(test);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }

        public EditTaskViewModel MapToEditTaskViewModel(Data.Models.Task model)
            => new EditTaskViewModel
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Tests = model.Tests
            };
    }
}
