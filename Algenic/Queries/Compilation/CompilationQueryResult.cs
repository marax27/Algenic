using Algenic.Compilation.Outputs;

namespace Algenic.Queries.Compilation
{
    public class CompilationQueryResult
    {
        public JDoodleOutput Output { get; set; }
        public JDoodleError Error { get; set; }
        public bool ExecutionSuccessful { get; set; }
    }
}
