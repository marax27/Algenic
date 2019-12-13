using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel;

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

    public static class ContestStatusNames
    {
        public static string GetReadableName(Contest.ContestState state)
        {
            switch (state)
            {
                case Contest.ContestState.NotStarted: return "Not started";
                case Contest.ContestState.InProgress: return "In progress";
                case Contest.ContestState.Completed: return "Completed";
                default: throw new InvalidEnumArgumentException(state.ToString());
            }
        }
    }
}
