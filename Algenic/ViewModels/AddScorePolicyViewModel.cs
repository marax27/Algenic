using System.Collections.Generic;

namespace Algenic.ViewModels
{
    public class AddScorePolicyViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ScoreRuleViewModel> Values { get; set; } = new List<ScoreRuleViewModel>();
    }

    public class ScoreRuleViewModel
    {
        public decimal Threshold { get; set; }
        public int Points { get; set; }
    }
}
