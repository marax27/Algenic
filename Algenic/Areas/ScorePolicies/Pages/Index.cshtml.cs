using System.Linq;
using System.Threading.Tasks;
using Algenic.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Algenic.Areas.ScorePolicies.Pages
{
    [Authorize(Roles = "Examiner,Admin")]
    public class IndexModel : PageModel
    {
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
            return Page();
        }

        public void OnPostAddRule()
        {
            UpdateFormViewModel();
        }

        private void UpdateFormViewModel()
        {
            FormPolicy.Values = Enumerable.Range(0, RuleCount).Select(_ => new ScoreRuleViewModel()).ToList();
        }
    }
}
