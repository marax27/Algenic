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
    public class JDoodleCompiler : IRemoteCompiler<JDoodleOutput, JDoodleError>
    {
        private static readonly string apiUrl = @"https://api.jdoodle.com/v1/execute";

        private readonly ClientConfiguration _clientConfiguration;
        private readonly HttpClient _httpClient;

        public JDoodleOutput Output { get; private set; }
        public JDoodleError ErrorOutput { get; private set; }
        public bool IsSuccessful { get; set; }

        public JDoodleCompiler(ClientConfiguration clientConfiguration)
        {
            _clientConfiguration = clientConfiguration;
            _httpClient = new HttpClient();
        }

        public async Task CompileAsync(string sourceCode, ProgrammingLanguage programmingLanguage)
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
            try
            {
                var response = await _httpClient.PostAsync(new Uri(apiUrl), content);
                var jsonCode = response.Content.ReadAsStringAsync();
                Output = JsonConvert.DeserializeObject<JDoodleOutput>(jsonCode.Result);
                IsSuccessful = true;
            }
            catch (ArgumentNullException)
            {
                ErrorOutput.Error = "offline";
                ErrorOutput.StatusCode = "400";
                IsSuccessful = false;
            }
            catch (HttpRequestException)
            {
                ErrorOutput.Error = "offline";
                ErrorOutput.StatusCode = "400";
                IsSuccessful = false;
            }
            catch (JsonException)
            {
                var response = await _httpClient.PostAsync(new Uri(apiUrl), content);
                var jsonCode = response.Content.ReadAsStringAsync();
                ErrorOutput = JsonConvert.DeserializeObject<JDoodleError>(jsonCode.Result);
                IsSuccessful = false;
            }
        }
    }
}
