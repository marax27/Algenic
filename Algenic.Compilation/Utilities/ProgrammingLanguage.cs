using System;

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

    public static class ProgrammingLanguageFactory
    {
        public static ProgrammingLanguage Get(string programmingLanguage)
        {
            switch (programmingLanguage.ToUpper())
            {
                case "CPP":
                case "C++":
                    return ProgrammingLanguage.CPlusPlus();
                case "C":
                    return ProgrammingLanguage.PlainC();
                case "JAVA":
                    return ProgrammingLanguage.Java();
                default: throw new ProgrammingLanguageNotFoundException(programmingLanguage);
            }
        }
    }

    public class ProgrammingLanguageNotFoundException : Exception
    {
        public ProgrammingLanguageNotFoundException(string language)
            : base($"Cannot associate any supported language with '{language}'.") { }
    }
}
