using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechZone.Core.Entities;
using TechZone.Core.Entities.Laptop;

namespace TechZone.Core.EntityConfigs
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.ToTable("Brands");
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(b => b.Country)
                   .HasMaxLength(100);

            builder.Property(b => b.LogoUrl)
                   .HasMaxLength(500);

            builder.Property(b => b.Description)
                   .HasColumnType("text");
        }
    }
}