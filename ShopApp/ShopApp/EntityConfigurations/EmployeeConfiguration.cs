using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ShopApp.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShopApp.EntityConfigurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employee").HasKey(p => p.EmployeeId);
            builder.Property(p => p.EmployeeId).IsRequired().HasColumnName("EmployeeId").ValueGeneratedOnAdd();
            builder.Property(p => p.FirstName).IsRequired().HasColumnName("FirstName").HasMaxLength(50);
            builder.Property(p => p.LastName).IsRequired().HasColumnName("LastName").HasMaxLength(50);
            builder.Property(p => p.HiredDate).IsRequired().HasColumnName("HiredDate").HasMaxLength(7);
            builder.Property(p => p.DateOfBirth).HasColumnName("DateOfBirth");

            builder.HasOne(d => d.Office)
                .WithMany(p => p.Employees)
                .HasForeignKey(d => d.OfficeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(d => d.Title)
                .WithMany(p => p.Employees)
                .HasForeignKey(d => d.TitleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(new List<Employee>()
            {
                new Employee() { EmployeeId = 1, FirstName = "David", LastName = "Novak", HiredDate = new DateTime(2008, 5, 1, 8, 30, 52), OfficeId = 1, TitleId = 1 },
                new Employee() { EmployeeId = 2, FirstName = "Nick", LastName = "Pratt", HiredDate = new DateTime(2008, 5, 1, 8, 30, 52), OfficeId = 1, TitleId = 1 },
                new Employee() { EmployeeId = 3, FirstName = "David", LastName = "Novak", HiredDate = new DateTime(2008, 5, 1, 8, 30, 52), OfficeId = 1, TitleId = 1 },
            });
        }
    }
}
