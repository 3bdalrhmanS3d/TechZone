using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechZone.Core.Entities;

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

            builder.Property(b => b.IsActive)
                   .HasDefaultValue(true);

            builder.Property(b => b.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(b => b.UpdatedAt)
                   .IsRequired(false);

            builder.Property(b => b.DeletedAt)
                   .IsRequired(false);

            builder.Property(b => b.IsDeleted)
                   .HasDefaultValue(false);

            builder.HasQueryFilter(b => !b.IsDeleted);
        }
    }
}