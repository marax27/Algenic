using Algenic.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Algenic.Data.Configuration
{
    public class ScoreRuleConfiguration : IEntityTypeConfiguration<ScoreRule>
    {
        public void Configure(EntityTypeBuilder<ScoreRule> builder)
        {
            builder.HasKey(s => new { s.ScorePolicyId, s.Threshold, s.Score });

            builder.HasOne(s => s.ScorePolicy)
                .WithMany(sp => sp.ScoreRules)
                .HasForeignKey(s => s.ScorePolicyId);
        }
    }
}
