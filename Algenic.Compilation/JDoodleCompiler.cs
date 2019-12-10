using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Algenic.Compilation.Utilities;
using Algenic.Compilation.Outputs;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Algenic.Compilation
{
    public class JDoodleCompiler
    {
        private static readonly string apiUrl = @"https://api.jdoodle.com/v1/execute";

        private readonly ClientConfiguration _clientConfiguration;
        private readonly HttpClient _httpClient;

        public JDoodleCompiler(ClientConfiguration clientConfiguration)
        {
            _clientConfiguration = clientConfiguration;
            _httpClient = new HttpClient();
        }

        public async Task<string> Compile(string sourceCode, ProgrammingLanguage programmingLanguage)
        {
            var compilationRequest = new CompilationRequestBuilder()
                .WithClient(_clientConfiguration)
                .WithProgrammingLanguage(programmingLanguage)
                .WithSourceCode(sourceCode)
                .Build();

            var requestJson = JsonConvert.SerializeObject(compilationRequest, new JsonSerializerSettings()
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy
                    {
                        OverrideSpecifiedNames = false
                    }
                }
            });
            var content = new StringContent(requestJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(new Uri(apiUrl), content);
            var jsonCode = response.Content.ReadAsStringAsync();
            return procesResponse(jsonCode.Result);
        }
        private string procesResponse(string resultJson)
        {
            JDoodleOutput output = JsonConvert.DeserializeObject<JDoodleOutput>(resultJson);
            return output.Output;
        }
    }
}
