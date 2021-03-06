using Algenic.Commands.CreateContest;
using Algenic.Commands.VerifySolution;
using Algenic.Commons;
using Algenic.Compilation;
using Algenic.Compilation.Outputs;
using Algenic.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Algenic.Data;
using Algenic.Data.Initializers;
using Algenic.Queries.AggregateContestSolutions;
using Algenic.Queries.Compilation;
using Algenic.Queries.ContestOwner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Algenic.Commands.CreateSolution;
using Algenic.Queries.NewestSolutions;
using Algenic.Commands.ContestEnd;
using Algenic.Commands.ChangeScore;
using Algenic.Commands.CreateScorePolicy;
using Algenic.Commands.ExaminerRole;
using Algenic.Queries.AllScorePolicies;
using Algenic.Queries.AllUsers;
using Algenic.Queries.CalculateScore;
using Algenic.Queries.ContestsUsers;
using Algenic.Queries.TaskScore;
using Algenic.Queries.TestResults;
using Algenic.Queries.ContestScoreQuery;

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
            services.AddTransient<ICommandHandler<CreateSolutionCommand>, CreateSolutionCommandHandler>();
            services.AddTransient<ICommandHandler<VerifySolutionCommand>, VerifySolutionCommandHandler>();
            services.AddTransient<ICommandHandler<ContestEndCommand>, ContestEndCommandHandler>();
            services.AddTransient<ICommandHandler<ChangeScoreCommand>, ChangeScoreCommandHandler>();
            services.AddTransient<ICommandHandler<CreateScorePolicyCommand>, CreateScorePolicyCommandHandler>();
            services.AddTransient<ICommandHandler<ExaminerRoleCommand>, ExaminerRoleCommandHandler>();

            services.AddTransient<IQueryHandler<ContestOwnerQuery, ContestOwnerResult>, ContestOwnerQueryHandler>();
            services.AddTransient<IQueryHandler<CompilationQuery, CompilationQueryResult>, CompilationQueryHandler>();
            services.AddTransient<IQueryHandler<AggregateContestSolutionsQuery, AggregateContestSolutionsResult>,
                AggregateContestSolutionsQueryHandler>();
            services.AddTransient<IQueryHandler<NewestSolutionsQuery, NewestSolutionsResult>,
                NewestSolutionsQueryHandler>();
            services.AddTransient<IQueryHandler<CalculateScoreQuery, CalculateScoreResult>, CalculateScoreQueryHandler>();
            services.AddTransient<IQueryHandler<AllScorePoliciesQuery, AllScorePoliciesResult>, AllScorePoliciesQueryHandler>();
            services.AddTransient<IQueryHandler<AllUsersQuery, AllUsersResult>, AllUsersQueryHandler>();
            services.AddTransient<IQueryHandler<ContestsUsersQuery, ContestsUsersQueryResult>, ContestsUsersQueryHandler>();
            services.AddTransient<IQueryHandler<TaskScoreQuery, TaskScoreQueryResult>, TaskScoreQueryHandler>();
            services.AddTransient<IQueryHandler<TestResultsQuery, TestResultsQueryResult>, TestResultsQueryHandler>();
            services.AddTransient<IQueryHandler<ContestScoreQuery, ContestScoreQueryResult>, ContestScoreQueryHandler>();

            services.AddTransient<IRemoteCompiler<JDoodleOutput, JDoodleError>, ConfigurableJDoodleCompiler>();
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
