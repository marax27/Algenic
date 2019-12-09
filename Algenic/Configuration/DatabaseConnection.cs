using Algenic.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Proxies;

namespace Algenic.Configuration
{
    public static class DatabaseConnection
    {
        public static IServiceCollection ConfigureDatabaseConnection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")).UseLazyLoadingProxies());
            return services;
        }
    }
}
