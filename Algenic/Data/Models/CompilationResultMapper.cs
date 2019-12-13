using Algenic.Compilation.Outputs;
namespace Algenic.Data.Models
{
    public class CompilationResultMapper
    {
        public CompilationResult Map(JDoodleOutput output)
        {
            CompilationResult result = new CompilationResult();
            result.CpuTime = output.CpuTime;
            result.MemoryUsage = output.Memory;
            if (output.StatusCode == "200") result.ExecutionSuccessful = true;
            else result.ExecutionSuccessful = false;
            result.Output = output.Output;
            return result;
        }
    }
}
