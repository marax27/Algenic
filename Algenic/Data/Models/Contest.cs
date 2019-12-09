using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Algenic.Data.Models
{
    public class Contest
    {
        public enum ContestState
        {
            NotStarted,
            InProgress,
            Completed
        }

        public int Id { get; set; }
        public ContestState Status { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
        public virtual IdentityUser IdentityUser { get; set; }
    }
}
