using Algenic.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Algenic.Data.Configuration
{
    public class CompilationResultConfiguration : IEntityTypeConfiguration<CompilationResult>
    {
        public void Configure(EntityTypeBuilder<CompilationResult> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.Solution)
                .WithMany(s => s.CompilationResults)
                .HasForeignKey(c => c.SolutionId);

            builder.HasOne(l => l.Test)
                .WithMany()
                .HasForeignKey(l => l.TestId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
