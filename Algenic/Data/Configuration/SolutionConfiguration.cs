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

            /*builder.HasOne(s => s.Task)
                .WithMany(t => t.Solutions)
                .HasForeignKey(s => s.TaskId);*/

            builder.HasOne(s => s.CompilationResult)
                .WithOne(c => c.Solution)
                .HasForeignKey<Solution>(s => s.CompilationResultId);
        }
    }
}
