namespace Algenic.Compilation.Utilities
{
    public class ProgrammingLanguage
    {
        public string LanguageCode { get; }
        public int VersionIndex { get; }

        private ProgrammingLanguage(string languageCode, int versionIndex)
        {
            LanguageCode = languageCode;
            VersionIndex = versionIndex;
        }

        public static ProgrammingLanguage CPlusPlus() =>
            new ProgrammingLanguage("cpp", 4);

        public static ProgrammingLanguage Java() =>
            new ProgrammingLanguage("java", 3);

        public static ProgrammingLanguage PlainC() =>
            new ProgrammingLanguage("c", 4);
    }
}
