namespace Algenic.Compilation.Utilities
{
    public class CompilationRequest
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Script { get; set; }
        public string Stdin { get; set; }
        public string Language { get; set; }
        public int VersionIndex { get; set; }
    }

    public class CompilationRequestBuilder
    {
        private string _clientId;
        private string _clientSecret;
        private string _script;
        private string _stdin;
        private string _language;
        private int _versionIndex;

        public CompilationRequestBuilder WithClient(ClientConfiguration clientConfiguration)
        {
            _clientId = clientConfiguration.ClientId;
            _clientSecret = clientConfiguration.ClientSecret;
            return this;
        }

        public CompilationRequestBuilder WithSourceCode(string sourceCode)
        {
            _script = sourceCode;
            return this;
        }

        public CompilationRequestBuilder WithStandardInput(string standardInput)
        {
            _stdin = standardInput;
            return this;
        }

        public CompilationRequestBuilder WithProgrammingLanguage(ProgrammingLanguage language)
        {
            _language = language.LanguageCode;
            _versionIndex = language.VersionIndex;
            return this;
        }

        public CompilationRequest Build() =>
            new CompilationRequest
            {
                ClientId = _clientId,
                ClientSecret = _clientSecret,
                Language = _language,
                Script = _script,
                Stdin = _stdin,
                VersionIndex = _versionIndex
            };
    }
}
