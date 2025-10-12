using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechZone.Domain.Entities;

namespace TechZone.Infrastructure.EntityConfigs
{
    public class AccessoryTypeConfiguration : IEntityTypeConfiguration<AccessoryType>
    {
        public void Configure(EntityTypeBuilder<AccessoryType> builder)
        {
            builder.ToTable("AccessoryTypes");
            builder.HasKey(at => at.Id);

            builder.Property(at => at.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(at => at.Description)
                   .HasColumnType("text");

            builder.Property(at => at.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("TIMEZONE('utc', NOW())"); // Changed from GETUTCDATE()

            builder.Property(at => at.UpdatedAt)
                   .IsRequired(false);

            builder.Property(at => at.DeletedAt)
                   .IsRequired(false);

            builder.Property(at => at.IsDeleted)
                   .HasDefaultValue(false);

            builder.HasQueryFilter(at => !at.IsDeleted);

            // Index
            builder.HasIndex(at => at.Name).IsUnique();
        }
    }
}