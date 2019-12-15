using System.Linq;
using Algenic.Commons;
using Algenic.Compilation;
using Algenic.Compilation.Outputs;
using Algenic.Compilation.Utilities;
using Algenic.Data;
using Algenic.Data.Mappers;
using Algenic.Mappers;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Algenic.Commands.VerifySolution
{
    public class VerifySolutionCommandHandler : ICommandHandler<VerifySolutionCommand>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IRemoteCompiler<JDoodleOutput, JDoodleError> _compiler;

        public VerifySolutionCommandHandler(ApplicationDbContext dbContext,
            IRemoteCompiler<JDoodleOutput, JDoodleError> compiler)
        {
            _compiler = compiler;
        }

        public async Task HandleAsync(VerifySolutionCommand command)
        {
            var solution = await _dbContext.Solutions.SingleAsync(s => s.Id == command.SolutionId);
            var tests = solution.Task.Tests.ToList();
            var programmingLanguage = ProgrammingLanguageFactory.Get(solution.Language);

            foreach (var test in tests)
            {
                await _compiler.CompileAsync(solution.SourceCode, test.Input, programmingLanguage);
                if (_compiler.IsSuccessful)
                {
                    var compilationResult = new CompilationResultMapper(_compiler.Output).Map();
                }
                else
                {
                    var log = new LogMapper(_compiler.ErrorOutput).Map();
                }
            }
        }
    }
}
