using System.Linq;
using System.Threading.Tasks;
using Algenic.Commands.CreateScorePolicy;
using Algenic.Commons;
using Algenic.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Algenic.Areas.ScorePolicies.Pages
{
    [Authorize(Roles = "Examiner,Admin")]
    public class IndexModel : PageModel
    {
        private readonly ICommandHandler<CreateScorePolicyCommand> _createScorePolicyCommandHandler;

        public IndexModel(ICommandHandler<CreateScorePolicyCommand> createScorePolicyCommandHandler)
        {
            _createScorePolicyCommandHandler = createScorePolicyCommandHandler;
        }

        [BindProperty]
        public AddScorePolicyViewModel FormPolicy { get; set; } = new AddScorePolicyViewModel();
        [BindProperty]
        public int RuleCount { get; set; } = 1;

        public void OnGet()
        {
            UpdateFormViewModel();
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            var scoreRules = FormPolicy.Values
                .Select(v => new ScoreRuleDto(
                        v.Threshold > 1m ? v.Threshold / 100 : v.Threshold,
                        v.Points
                    )
                ).ToList();
            var command = CreateScorePolicyCommand.Create(
                FormPolicy.Name,
                FormPolicy.Description,
                scoreRules);
            await _createScorePolicyCommandHandler.HandleAsync(command);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostAddRuleAsync()
        {
            UpdateFormViewModel();
            return Page();
        }

        private void UpdateFormViewModel()
        {
            FormPolicy.Values = Enumerable.Range(0, RuleCount).Select(_ => new ScoreRuleViewModel()).ToList();
        }
    }
}
