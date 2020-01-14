using Algenic.Commons.DesignByContract;
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
        {
            Fail.If(!int.TryParse(_viewModel.ScorePolicyId, out var policyId),
                "Provided policy ID is invalid");

            return new Task
            {
                Name = _viewModel.Name,
                Description = _viewModel.Description,
                ScorePolicyId = policyId
            };
        }
    }
}
