using Microsoft.EntityFrameworkCore;
using ShopApp.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShopApp.EntityConfigurations
{
    public class TitleConfiguration : IEntityTypeConfiguration<Title>
    {
        public void Configure(EntityTypeBuilder<Title> builder)
        {
            builder.ToTable("Title").HasKey(p => p.TitleId);
            builder.Property(p => p.TitleId).IsRequired().HasColumnName("TitleId").ValueGeneratedOnAdd();
            builder.Property(p => p.Name).IsRequired().HasColumnName("Name").HasMaxLength(50);
        }
    }
}
