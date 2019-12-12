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
        public JDoodleOutput JDoodleOutput { get; set; }
        public JDoodleError JDoodleError { get; set; }
        public bool Flag { get; set; }

        public JDoodleCompiler(ClientConfiguration clientConfiguration)
        {
            _clientConfiguration = clientConfiguration;
            _httpClient = new HttpClient();
        }

        public async Task Compile(string sourceCode, ProgrammingLanguage programmingLanguage)
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
                JDoodleOutput = JsonConvert.DeserializeObject<JDoodleOutput>(jsonCode.Result);
                Flag = true;
            }
            catch (ArgumentNullException)
            {
                JDoodleError.Error = "offline";
                JDoodleError.StatusCode = "400";
                Flag = false;
            }
            catch (HttpRequestException)
            {
                JDoodleError.Error = "offline";
                JDoodleError.StatusCode = "400";
                Flag = false;
            }
            catch (JsonException)
            {
                var response = await _httpClient.PostAsync(new Uri(apiUrl), content);
                var jsonCode = response.Content.ReadAsStringAsync();
                JDoodleError = JsonConvert.DeserializeObject<JDoodleError>(jsonCode.Result);
                Flag = false;
            }
        }
    }
}
