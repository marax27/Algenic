using Algenic.Commands.ChangeScore;
using Algenic.Commands.VerifySolution;
using Algenic.Commons;
using Algenic.Data.Models;
using Algenic.Queries.CalculateScore;
using Algenic.Queries.NewestSolutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Algenic.Commands.ContestEnd
{
    public class ContestEndCommandHandler : ICommandHandler<ContestEndCommand>
    {
        private readonly IQueryHandler<NewestSolutionsQuery, NewestSolutionsResult> _newestSolutionQueryHandler;
        private readonly IQueryHandler<CalculateScoreQuery, CalculateScoreResult> _calculateScoreQueryHandler;
        private readonly ICommandHandler<VerifySolutionCommand> _verifySolutionCommandHandler;
        private readonly ICommandHandler<ChangeScoreCommand> _changeScoreCommandHandler;

        public ContestEndCommandHandler(IQueryHandler<NewestSolutionsQuery, NewestSolutionsResult> newestSolutionQueryHandler,
            IQueryHandler<CalculateScoreQuery, CalculateScoreResult> calculateScoreQueryHandler,
            ICommandHandler<VerifySolutionCommand> verifySolutionCommandHandler,
            ICommandHandler<ChangeScoreCommand> changeScoreCommandHandler)
        {
            _newestSolutionQueryHandler = newestSolutionQueryHandler;
            _calculateScoreQueryHandler = calculateScoreQueryHandler;
            _verifySolutionCommandHandler = verifySolutionCommandHandler;
            _changeScoreCommandHandler = changeScoreCommandHandler;
        }

        public async System.Threading.Tasks.Task HandleAsync(ContestEndCommand command)
        {
            var newestSolutionsQuery = NewestSolutionsQuery.Create(command.ContestId);
            var solutions = await _newestSolutionQueryHandler.HandleAsync(newestSolutionsQuery);

            foreach (var solutionId in solutions.SolutionIds)
            {
                var verifySolutionCommand = VerifySolutionCommand.Create(solutionId);
                await _verifySolutionCommandHandler.HandleAsync(verifySolutionCommand);

                var calculateScoreQuery = CalculateScoreQuery.Create(solutionId);
                var calculateScoreResult = await _calculateScoreQueryHandler.HandleAsync(calculateScoreQuery);

                var changeScoreCommand = ChangeScoreCommand.Create(solutionId, calculateScoreResult.Score);
                await _changeScoreCommandHandler.HandleAsync(changeScoreCommand);
            }
        }
    }
}
