using Algenic.Data.Models;
using Algenic.ViewModels;

namespace Algenic.Mappers
{
    public class TaskMapper
    {
        private readonly AddTaskViewModel _viewModel;

        public TaskMapper(AddTaskViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public Task Map()
            => new Task
            {
                Name = _viewModel.Name,
                Description = _viewModel.Description
            };
    }
}
