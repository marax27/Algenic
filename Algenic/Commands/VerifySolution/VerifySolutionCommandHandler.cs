using System.Linq;
using System.Threading.Tasks;
using Algenic.Commons;
using Algenic.Compilation;
using Algenic.Compilation.Outputs;
using Algenic.Data;
using Microsoft.EntityFrameworkCore;

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
        }
    }
}
