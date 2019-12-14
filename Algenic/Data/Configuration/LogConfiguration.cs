using Algenic.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Algenic.Data.Configuration
{
    public class LogConfiguration : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.HasKey(l => l.Id);

            builder.HasOne(l => l.Solution)
                .WithOne(s => s.Log)
                .HasForeignKey<Log>(l => l.SolutionId);

            builder.HasOne(l => l.Test)
                .WithMany()
                .HasForeignKey(l => l.TestId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
