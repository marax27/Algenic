using Algenic.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using static Algenic.Data.Models.Contest;

namespace Algenic.ViewModels
{
    public class DisplayContestViewModel
    {
        public string Name { get; set; }
        public string OwnerId { get; set; }
        public ContestState Status { get; set; }
        public bool NotStarted { get; set; }

        public ICollection<Task> Tasks { get; set; }
    }
}
