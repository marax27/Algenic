using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Algenic.Configuration
{
    public static class Mvc
    {
        public static IServiceCollection ConfigureMvc(this IServiceCollection services)
        {
            services.AddMvc()
                    .AddRazorPagesOptions(options =>
                    {
                        options.Conventions.AuthorizeAreaFolder("Admin", "/", "MustBeAdmin");
                    })
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            return services;
        }
    }
}
