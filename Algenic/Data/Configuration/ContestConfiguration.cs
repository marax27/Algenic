using Algenic.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Algenic.Data.Models.Contest;

namespace Algenic.Data.Configuration
{
    public class ContestConfiguration : IEntityTypeConfiguration<Contest>
    {
        public void Configure(EntityTypeBuilder<Contest> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Status)
                .HasConversion(new EnumToStringConverter<ContestState>());

            builder.Property(c => c.Name)
                .IsRequired();
        }
    }
}
