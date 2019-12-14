using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Algenic.Data.Models
{
    public class Log
    {
        public int Id { get; set; }
        public int SolutionId { get; set; }
        public int TestId { get; set; }
        public string ErrorMessage { get; set; }

        public virtual Solution Solution { get; set; }
        public virtual Test Test { get; set; }
    }
}
