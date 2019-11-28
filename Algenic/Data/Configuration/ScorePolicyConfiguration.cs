using Algenic.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Algenic.Data.Configuration
{
    public class ScorePolicyConfiguration : IEntityTypeConfiguration<ScorePolicy>
    {
        public void Configure(EntityTypeBuilder<ScorePolicy> builder)
        {
            builder.HasKey(s => s.Id);

            builder.HasMany(s => s.Tasks)
                .WithOne(t => t.ScorePolicy)
                .HasForeignKey(t => t.ScorePolicyId);

            builder.HasMany(s => s.ScoreRules)
                .WithOne(sr => sr.ScorePolicy)
                .HasForeignKey(sr => sr.ScorePolicyId);
        }
    }
}
