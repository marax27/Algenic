using Algenic.Commons.DesignByContract;
using Algenic.Compilation.Utilities;

namespace Algenic.Queries.Compilation
{
    public class CompilationQuery
    {
        public string SourceCode { get; }
        public string Input { get; }
        public ProgrammingLanguage ProgrammingLanguage { get; }

        private CompilationQuery(string sourceCode, string input, ProgrammingLanguage language)
        {
            SourceCode = sourceCode;
            Input = input;
            ProgrammingLanguage = language;
        }

        public static CompilationQuery Create(string sourceCode, string input, ProgrammingLanguage language)
        {
            Fail.IfNullOrEmpty(sourceCode);
            Fail.IfNull(input);
            Fail.IfNull(language);

            return new CompilationQuery(sourceCode, input, language);
        }
    }
}
