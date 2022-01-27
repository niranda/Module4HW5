using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ShopApp.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShopApp.EntityConfigurations
{
    public class EmployeeProjectConfiguration : IEntityTypeConfiguration<EmployeeProject>
    {
        public void Configure(EntityTypeBuilder<EmployeeProject> builder)
        {
            builder.ToTable("EmployeeProject").HasKey(p => p.EmployeeProjectId);
            builder.Property(p => p.EmployeeProjectId).IsRequired().HasColumnName("EmployeeProjectId").ValueGeneratedOnAdd();
            builder.Property(p => p.Rate).IsRequired().HasColumnName("Rate");
            builder.Property(p => p.StartedDate).IsRequired().HasColumnName("StartedDate").HasMaxLength(7);

            builder.HasOne(d => d.Employee)
                .WithMany(p => p.EmployeeProjects)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(d => d.Project)
                .WithMany(p => p.EmployeeProjects)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(new List<EmployeeProject>()
            {
                new EmployeeProject() { EmployeeProjectId = 1, Rate = 3.78M, StartedDate = new DateTime(2008, 5, 1, 8, 30, 52), EmployeeId = 1, ProjectId = 1 },
                new EmployeeProject() { EmployeeProjectId = 2, Rate = 4.58M, StartedDate = new DateTime(2008, 5, 1, 8, 30, 52), EmployeeId = 1, ProjectId = 1 },
                new EmployeeProject() { EmployeeProjectId = 3, Rate = 5.68M, StartedDate = new DateTime(2008, 5, 1, 8, 30, 52), EmployeeId = 1, ProjectId = 1 }
            });
        }
    }
}
