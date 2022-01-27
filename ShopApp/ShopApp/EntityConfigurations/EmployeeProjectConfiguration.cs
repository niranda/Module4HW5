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
        }
    }
}
