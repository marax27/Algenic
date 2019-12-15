﻿using System.Linq;
using Algenic.Commons;
using Algenic.Compilation;
using Algenic.Compilation.Outputs;
using Algenic.Compilation.Utilities;
using Algenic.Data;
using Algenic.Data.Mappers;
using Algenic.Mappers;
using Algenic.Queries.Compilation;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Algenic.Commands.VerifySolution
{
    public class VerifySolutionCommandHandler : ICommandHandler<VerifySolutionCommand>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IQueryHandler<CompilationQuery, CompilationQueryResult> _compilationQueryHandler;

        public VerifySolutionCommandHandler(ApplicationDbContext dbContext,
            IQueryHandler<CompilationQuery, CompilationQueryResult> compilationQueryHandler)
        {
            _compilationQueryHandler = compilationQueryHandler;
        }

        public async Task HandleAsync(VerifySolutionCommand command)
        {
            var solution = await _dbContext.Solutions.SingleAsync(s => s.Id == command.SolutionId);
            var tests = solution.Task.Tests.ToList();
            var programmingLanguage = ProgrammingLanguageFactory.Get(solution.Language);

            foreach (var test in tests)
            {
                var query = CompilationQuery.Create(solution.SourceCode, test.Input, programmingLanguage);
                var queryResult = await _compilationQueryHandler.HandleAsync(query);

                if (queryResult.ExecutionSuccessful)
                {
                    var compilationResult = new CompilationResultMapper(queryResult.Output).Map();
                }
                else
                {
                    var log = new LogMapper(queryResult.Error).Map();
                }
            }
        }
    }
}
