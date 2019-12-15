using Algenic.Compilation.Outputs;
using Algenic.Data.Models;
namespace Algenic.Data.Mappers
{
    public class CompilationResultMapper
    {
        private readonly JDoodleOutput _output;
        public CompilationResultMapper(JDoodleOutput output)
        {
            _output = output;
        }
        public CompilationResult Map()
            => new CompilationResult()
            {
            CpuTime = _output.CpuTime,
            MemoryUsage = _output.Memory,
            ExecutionSuccessful = _output.StatusCode == "200",
            Output = _output.Output
            };
    }
}
