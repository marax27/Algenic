using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Algenic.Data.Models
{
    public class ScoreRule
    {
        public int ScorePolicyId { get; set; }
        public decimal Threshold { get; set; }
        public int Score { get; set; }

        public virtual ScorePolicy ScorePolicy { get; set; }
    }
}
