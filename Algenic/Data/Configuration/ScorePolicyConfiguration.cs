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

            builder.Property(s => s.Name)
                .IsRequired();
        }
    }
}
