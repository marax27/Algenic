using System.Threading.Tasks;
using Algenic.Commons;
using Algenic.Compilation;
using Algenic.Compilation.Outputs;

namespace Algenic.Queries.Compilation
{
    public class CompilationQueryHandler : IQueryHandler<CompilationQuery, CompilationQueryResult>
    {
        private readonly IRemoteCompiler<JDoodleOutput, JDoodleError> _compiler;

        public CompilationQueryHandler(IRemoteCompiler<JDoodleOutput, JDoodleError> compiler)
        {
            _compiler = compiler;
        }

        public async Task<CompilationQueryResult> HandleAsync(CompilationQuery query)
        {
            await _compiler.CompileAsync(query.SourceCode, query.Input, query.ProgrammingLanguage);
            return new CompilationQueryResult
            {
                ExecutionSuccessful = _compiler.IsSuccessful,
                Output = _compiler.Output,
                Error = _compiler.ErrorOutput
            };
        }
    }
}
