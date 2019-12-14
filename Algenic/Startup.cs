using Algenic.Commands.CreateContest;
using Algenic.Commons;
using Algenic.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Algenic.Data;
using Algenic.Data.Initializers;
using Algenic.Queries.ContestOwner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Algenic
{
    public class Startup
    {
        private readonly IHostingEnvironment _environment;

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            _environment = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureCookiePolicy()
                    .ConfigureDatabaseConnection(Configuration)
                    .AddDefaultIdentity<IdentityUser>()
                    .AddRoles<IdentityRole>()
                    .AddDefaultUI(UIFramework.Bootstrap4)
                    .AddEntityFrameworkStores<ApplicationDbContext>();

            if (_environment.IsDevelopment())
            {
                services.ConfigureSimplePasswordPolicy();
            }

            services.AddAuthorization(option =>
            {
                option.AddPolicy("MustBeAdmin", policy =>
                    policy.RequireRole("Admin"));
            });

            services.ConfigureMvc();

            services.AddTransient<ICommandHandler<CreateContestCommand>, CreateContestCommandHandler>();

            services.AddTransient<IQueryHandler<ContestOwnerQuery, ContestOwnerResult>, ContestOwnerQueryHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, UserManager<IdentityUser> userManager)
        {
            if (_environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage()
                   .UseDatabaseErrorPage();

                IdentityUserInitializer.SeedUsers(userManager);
            }
            else
            {
                app.UseHttpsRedirection()
                   .UseExceptionHandler("/Error")
                   .UseHsts();  // the default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            }

            app.UseStaticFiles()
               .UseCookiePolicy()
               .UseAuthentication()
               .UseMvc();
        }
    }
}
