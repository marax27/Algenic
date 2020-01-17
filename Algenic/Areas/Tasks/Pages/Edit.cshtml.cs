using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Algenic.Commons;
using Algenic.Data;
using Algenic.Data.Models;
using Algenic.Queries.AllScorePolicies;
using Algenic.Routing;
using Algenic.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Algenic.Areas.Tasks.Pages
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IQueryHandler<AllScorePoliciesQuery, AllScorePoliciesResult>
            _allScorePoliciesQueryHandler;

        [TempData]
        public int TaskId { get; set; }
        [BindProperty]
        public EditTaskViewModel FormTask { get; set; }
        [BindProperty]
        public AddTestViewModel FormTest { get; set; }
        [BindProperty]
        public IEnumerable<SelectListItem> ScorePolicyOptions { get; set; }

        public EditModel(ApplicationDbContext context, UserManager<IdentityUser> userManager,
            IQueryHandler<AllScorePoliciesQuery, AllScorePoliciesResult> allScorePoliciesQueryHandler)
        {
            _context = context;
            _userManager = userManager;
            _allScorePoliciesQueryHandler = allScorePoliciesQueryHandler;
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

            UpdateScorePolicies();

            return Page();
        }

        public async Task<IActionResult> OnPostChangeAsync()
        {
            var task = await _context.Tasks.FindAsync(TaskId);
            task.Name = FormTask.Name;
            task.Description = FormTask.Description;
            task.ScorePolicy = _context.ScorePolicies.Find(FormTask.ScorePolicyId);
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

        private void UpdateScorePolicies()
        {
            var query = AllScorePoliciesQuery.Create();
            var queryResult = _allScorePoliciesQueryHandler.HandleAsync(query).Result;

            var currentPolicyId = _context.Tasks.Find(TaskId).ScorePolicyId;

            ScorePolicyOptions = queryResult.ScorePolicies
                .OrderBy(sp => sp.Id == currentPolicyId ? 0 : 1)
                .Select(
                    policy => new SelectListItem
                    {
                        Value = policy.Id.ToString(),
                        Text = $"{policy.Name} ({policy.Description})"
                    });
        }
    }
}
