using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ShopApp.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShopApp.EntityConfigurations
{
    public class OfficeConfiguration : IEntityTypeConfiguration<Office>
    {
        public void Configure(EntityTypeBuilder<Office> builder)
        {
            builder.ToTable("Office").HasKey(p => p.OfficeId);
            builder.Property(p => p.OfficeId).IsRequired().HasColumnName("OfficeId").ValueGeneratedOnAdd();
            builder.Property(p => p.Title).IsRequired().HasColumnName("Title").HasMaxLength(100);
            builder.Property(p => p.Location).IsRequired().HasColumnName("Location").HasMaxLength(100);

            builder.HasData(new List<Office>()
            {
                new Office() { OfficeId = 1, Title = "First Office", Location = "Kyiv" },
                new Office() { OfficeId = 2, Title = "Second Office", Location = "Madrid" },
                new Office() { OfficeId = 3, Title = "Third Office", Location = "Manchester" }
            });
        }
    }
}
