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
using Algenic.Commons;
using Algenic.Mappers;
using Algenic.Queries.AggregateContestSolutions;
using Algenic.ViewModels;
using static Algenic.Data.Models.Contest;
using Algenic.Commands.ContestEnd;
using Algenic.Queries.AllScorePolicies;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Algenic.Areas.Contests.Pages
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        private readonly IQueryHandler<AggregateContestSolutionsQuery, AggregateContestSolutionsResult>
            _aggregateContestSolutionsQueryHandler;
        private readonly IQueryHandler<AllScorePoliciesQuery, AllScorePoliciesResult>
            _allScorePoliciesQueryHandler;

        private readonly ICommandHandler<ContestEndCommand> _contestEndCommandHandler;

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
        [BindProperty]
        public ContestSolutionAggregate ContestSolutions { get; set; }
        [BindProperty]
        public IEnumerable<SelectListItem> ScorePolicyOptions { get; set; }

        public EditModel(ApplicationDbContext context,
                         UserManager<IdentityUser> userManager,
                         IQueryHandler<AggregateContestSolutionsQuery, AggregateContestSolutionsResult> aggregateContestSolutionsQueryHandler,
                         IQueryHandler<AllScorePoliciesQuery, AllScorePoliciesResult> allScorePoliciesQueryHandler,
                         ICommandHandler<ContestEndCommand> contestEndCommandHandler)
        {
            _context = context;
            _userManager = userManager;
            _aggregateContestSolutionsQueryHandler = aggregateContestSolutionsQueryHandler;
            _allScorePoliciesQueryHandler = allScorePoliciesQueryHandler;
            _contestEndCommandHandler = contestEndCommandHandler;
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

            UpdateContestSolutions();
            UpdateScorePolicies();

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

            if(newStatus == ContestState.Completed)
            {
                var contestEndCommand = ContestEndCommand.Create(contest.Id);
                await _contestEndCommandHandler.HandleAsync(contestEndCommand);
            }

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

        private void UpdateContestSolutions()
        {
            var query = AggregateContestSolutionsQuery.Create(ContestId);
            var queryResult = _aggregateContestSolutionsQueryHandler.HandleAsync(query).Result;
            ContestSolutions = queryResult.Aggregate;
        }

        private void UpdateScorePolicies()
        {
            var query = AllScorePoliciesQuery.Create();
            var queryResult = _allScorePoliciesQueryHandler.HandleAsync(query).Result;
            ScorePolicyOptions = queryResult.ScorePolicies.Select(
                policy => new SelectListItem
                {
                    Value = policy.Id.ToString(),
                    Text = $"{policy.Name} ({policy.Description})"
                });
        }
    }
}
