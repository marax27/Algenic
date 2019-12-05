using Algenic.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Algenic.Data.Configuration
{
    public class TestConfiguration : IEntityTypeConfiguration<Test>
    {
        public void Configure(EntityTypeBuilder<Test> builder)
        {
            builder.HasKey(t => t.Id);

            builder.HasOne(t => t.Task)
                .WithMany(tsk => tsk.Tests)
                .HasForeignKey(t => t.TaskId);

            builder.Property(t => t.Name)
                .IsRequired();

            builder.Property(t => t.Input)
                .IsRequired();

            builder.Property(t => t.ExpectedOutput)
                .IsRequired();
        }
    }
}
