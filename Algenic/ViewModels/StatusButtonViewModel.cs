using Algenic.Data.Models;

namespace Algenic.ViewModels
{
    public class StatusButtonViewModel
    {
        public string Label { get; set; }
        public Contest.ContestState NewState { get; set; }
    }
}