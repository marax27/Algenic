using Algenic.Compilation;
using Algenic.Compilation.Utilities;
using Microsoft.Extensions.Configuration;

namespace Algenic
{
    public class ConfigurableJDoodleCompiler : JDoodleCompiler
    {
        public ConfigurableJDoodleCompiler(IConfiguration configuration)
            : base(configuration.GetSection("JDoodle").Get<ClientConfiguration>()) { }
    }
}
