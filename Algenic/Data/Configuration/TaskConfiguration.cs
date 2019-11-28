using Algenic.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Algenic.Data.Configuration
{
    public class TaskConfiguration : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder.HasKey(t => t.Id);

            builder.HasOne(t => t.Contest)
                .WithMany(c => c.Tasks)
                .HasForeignKey(t => t.ContestId);

            /*builder.HasOne(t => t.ScorePolicy)
                .WithMany(s => s.Tasks)
                .HasForeignKey(t => t.ScorePolicyId);*/

            builder.HasMany(t => t.Solutions)
                .WithOne(s => s.Task)
                .HasForeignKey(s => s.TaskId);

            builder.HasMany(t => t.Tests)
                .WithOne(ts => ts.Task)
                .HasForeignKey(ts => ts.TaskId);
        }
    }
}
