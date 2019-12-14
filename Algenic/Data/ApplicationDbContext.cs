using Algenic.Data.Configuration;
using Algenic.Data.Initializers;
using Algenic.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Algenic.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Solution> Solutions { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<CompilationResult> CompilationResults { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Contest> Contests { get; set; }
        public DbSet<ScorePolicy> ScorePolicies { get; set; }
        public DbSet<ScoreRule> ScoreRules { get; set; }
        public DbSet<Log> Logs { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyDefaultRoles();

            builder.ApplyConfiguration(new CompilationResultConfiguration());
            builder.ApplyConfiguration(new ContestConfiguration());
            builder.ApplyConfiguration(new ScorePolicyConfiguration());
            builder.ApplyConfiguration(new ScoreRuleConfiguration());
            builder.ApplyConfiguration(new SolutionConfiguration());
            builder.ApplyConfiguration(new TaskConfiguration());
            builder.ApplyConfiguration(new TestConfiguration());
            builder.ApplyConfiguration(new LogConfiguration());

            builder.Entity<Solution>().ToTable(nameof(Solutions));
            builder.Entity<Task>().ToTable(nameof(Tasks));
            builder.Entity<CompilationResult>().ToTable(nameof(CompilationResults));
            builder.Entity<Test>().ToTable(nameof(Tests));
            builder.Entity<Contest>().ToTable(nameof(Contests));
            builder.Entity<ScorePolicy>().ToTable(nameof(ScorePolicies));
            builder.Entity<ScoreRule>().ToTable(nameof(ScoreRules));
            builder.Entity<Log>().ToTable(nameof(Logs));
        }
    }
}
