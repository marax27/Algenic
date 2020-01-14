using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Algenic.Data.Models
{
    public class Task
    {
        public int Id { get; set; }
        public int ContestId { get; set; }
        public int? ScorePolicyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Solution> Solutions { get; set; } = new HashSet<Solution>();
        public virtual ICollection<Test> Tests { get; set; } = new HashSet<Test>();

        public virtual Contest Contest { get; set; }

        public virtual ScorePolicy ScorePolicy { get; set; }
    }
}
