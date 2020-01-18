using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Algenic.Data.Models.Contest;

namespace Algenic.ViewModels
{
    public class SolveTaskViewModel
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public string ContestOwnerId { get; set; }
        public ContestState ContestStatus { get; set; }
    }
}
