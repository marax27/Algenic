using Algenic.Data.Models;

namespace Algenic.ViewModels
{
    public class ContestViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public bool CanJoin { get; set; }
        public bool CanEdit { get; set; }
        public Contest.ContestState Status { get; set; }
    }
}