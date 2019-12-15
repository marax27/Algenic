using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Algenic.Data;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Algenic.Routing;
using Algenic.Data.Models;
using System.ComponentModel;
using System.Linq;
using Algenic.Mappers;
using Algenic.ViewModels;
using static Algenic.Data.Models.Contest;
using Task = System.Threading.Tasks.Task;

namespace Algenic.Areas.Contests.Pages
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        [TempData]
        public int ContestId { get; set; }
        [BindProperty]
        public EditContestViewModel ContestViewModel { get; set; }
        [BindProperty]
        public AddTaskViewModel FormTask { get; set; }
        [BindProperty]
        public IEnumerable<DisplayTaskViewModel> TasksToDisplay { get; set; }
        [BindProperty]
        public IEnumerable<StatusButtonViewModel> StatusButtons { get; set; }

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

            ContestViewModel = CreateContestViewModel(contest);
            StatusButtons = CreateStatusButtons(contest.Status);
            FormTask = new AddTaskViewModel();
            TasksToDisplay = contest.Tasks.Select(MapToDisplayViewModel).ToList();

            ContestId = id; 
            TempData.Keep(nameof(ContestId));

            return Page();
        }

        public async Task<IActionResult> OnPostRenameAsync()
        {
            var contest = await _context.Contests.FindAsync(ContestId);
            contest.Name = ContestViewModel.Name;
            await _context.SaveChangesAsync();

            return RedirectToPage(ContestId);
        }

        public async Task<IActionResult> OnPostChangeStatusAsync(ContestState newStatus)
        {
            var contest = await _context.Contests.FindAsync(ContestId);
            contest.Status = newStatus;
            await _context.SaveChangesAsync();

            return RedirectToPage(ContestId);
        }

        public async Task<IActionResult> OnPostAddTaskAsync()
        {
            var mapper = new TaskMapper(FormTask);
            var taskModel = mapper.Map();

            var contest = await _context.Contests.FindAsync(ContestId);
            contest.Tasks.Add(taskModel);
            await _context.SaveChangesAsync();
            return RedirectToPage(ContestId);
        }

        public async Task<IActionResult> OnPostEditTaskAsync(int taskId)
        {
            return RedirectToPage("Edit", new { area = "Tasks", id = taskId });
        }

        public async Task<IActionResult> OnPostRemoveTaskAsync(int taskId)
        {
            var contest = await _context.Contests.FindAsync(ContestId);
            var task = await _context.Tasks.FindAsync(taskId);
            contest.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return RedirectToPage(ContestId);
        }

        private IEnumerable<StatusButtonViewModel> CreateStatusButtons(ContestState contestStatus)
        {
            switch (contestStatus)
            {
                case ContestState.NotStarted:
                    return new[]
                    {
                        new StatusButtonViewModel {Label = "Start", NewState = ContestState.InProgress}
                    };
                case ContestState.InProgress:
                    return new[]
                    {
                        new StatusButtonViewModel {Label = "Complete", NewState = ContestState.Completed},
                        new StatusButtonViewModel {Label = "Cancel", NewState = ContestState.NotStarted}
                    };
                case ContestState.Completed:
                    return new[]
                    {
                        new StatusButtonViewModel {Label = "Restart", NewState = ContestState.InProgress}
                    };
                default: throw new InvalidEnumArgumentException(contestStatus.ToString());
            }
        }

        private EditContestViewModel CreateContestViewModel(Contest contest)
            => new EditContestViewModel
            {
                Name = contest.Name,
                Status = ContestStatusNames.GetReadableName(contest.Status),
                HasBegun = contest.Status != ContestState.NotStarted
            };

        private DisplayTaskViewModel MapToDisplayViewModel(Data.Models.Task model)
            => new DisplayTaskViewModel
            {
                Name = model.Name,
                Description = model.Description,
                Id = model.Id
            };
    }
}
