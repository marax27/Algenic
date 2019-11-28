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

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyDefaultRoles();
            builder.Entity<Solution>().ToTable(nameof(Solution));
            builder.Entity<Task>().ToTable(nameof(Task));
            builder.Entity<CompilationResult>().ToTable(nameof(CompilationResult));
            builder.Entity<Test>().ToTable(nameof(Test));
            builder.Entity<Contest>().ToTable(nameof(Contest));
            builder.Entity<ScorePolicy>().ToTable(nameof(ScorePolicy));
            builder.Entity<ScoreRule>().ToTable(nameof(ScoreRule));
        }
    }
}
