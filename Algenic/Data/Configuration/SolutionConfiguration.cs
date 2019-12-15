using Algenic.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Algenic.Data.Configuration
{
    public class SolutionConfiguration : IEntityTypeConfiguration<Solution>
    {
        public void Configure(EntityTypeBuilder<Solution> builder)
        {
            builder.HasKey(s => s.Id);

            builder.HasMany(s => s.CompilationResults)
                .WithOne(c => c.Solution)
                .HasForeignKey(c => c.SolutionId);

            builder.HasMany(s => s.Logs)
                .WithOne(l => l.Solution)
                .HasForeignKey(l => l.SolutionId);

            builder.HasOne(s => s.Task)
                .WithMany(t => t.Solutions)
                .HasForeignKey(s => s.TaskId);

            builder.Property(s => s.SourceCode)
                .IsRequired();
        }
    }
}
