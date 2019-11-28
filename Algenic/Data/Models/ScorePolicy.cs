using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Algenic.Data.Models
{
    public class ScorePolicy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ICollection<ScoreRule> ScoreRules { get; set; }
    }
}
