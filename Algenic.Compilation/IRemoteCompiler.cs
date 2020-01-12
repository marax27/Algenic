using System.Threading.Tasks;
using Algenic.Compilation.Utilities;

namespace Algenic.Compilation
{
    public interface IRemoteCompiler<out TOutput, out TErrorOutput>
    {
        Task CompileAsync(string sourceCode, string input, ProgrammingLanguage programmingLanguage);

        TOutput Output { get; }
        TErrorOutput ErrorOutput { get; }
        bool IsSuccessful { get; }
    }
}
