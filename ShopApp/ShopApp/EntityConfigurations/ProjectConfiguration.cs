using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ShopApp.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShopApp.EntityConfigurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Project").HasKey(p => p.ProjectId);
            builder.Property(p => p.ProjectId).IsRequired().ValueGeneratedOnAdd();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Description).IsRequired().HasMaxLength(150);
            builder.Property(p => p.Budget).IsRequired();
            builder.Property(p => p.StartedDate).IsRequired().HasMaxLength(7);

            builder.HasOne(d => d.Client)
                .WithMany(d => d.Projects)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(new List<Project>()
            {
                new Project() { ProjectId = 1, Name = "First Project", Description = "Some description", Budget = 34, StartedDate = new DateTime(2008, 5, 1, 8, 30, 52), ClientId = 1 },
                new Project() { ProjectId = 2, Name = "Second Project", Description = "Another description", Budget = 34, StartedDate = new DateTime(2008, 5, 1, 8, 30, 52), ClientId = 1 },
                new Project() { ProjectId = 3, Name = "Third Project", Description = "Different description", Budget = 34, StartedDate = new DateTime(2008, 5, 1, 8, 30, 52), ClientId = 1 }
            });
        }
    }
}
