using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Algenic.Data.Models
{
    public class Test
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string Name { get; set; }
        public string Input { get; set; }
        public string ExpectedOutput { get; set; }

        [ForeignKey(nameof(TaskId))]
        public virtual Task Task { get; set; }
    }
}
