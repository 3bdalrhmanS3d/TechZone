using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechZone.Core.Entities;

namespace TechZone.Core.EntityConfigs
{
    public class AccessoryAttributeConfiguration : IEntityTypeConfiguration<AccessoryAttribute>
    {
        public void Configure(EntityTypeBuilder<AccessoryAttribute> builder)
        {
            builder.ToTable("AccessoryAttributes");
            builder.HasKey(aa => aa.Id);

            builder.Property(aa => aa.AttributeKey)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(aa => aa.AttributeValue)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(aa => aa.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("TIMEZONE('utc', NOW())"); // Changed from GETUTCDATE()

            builder.Property(aa => aa.UpdatedAt)
                   .IsRequired(false);

            builder.Property(aa => aa.DeletedAt)
                   .IsRequired(false);

            builder.Property(aa => aa.IsDeleted)
                   .HasDefaultValue(false);

            builder.HasOne(aa => aa.Accessory)
                   .WithMany(a => a.Attributes)
                   .HasForeignKey(aa => aa.AccessoryId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasQueryFilter(aa => !aa.IsDeleted);

            // Index
            builder.HasIndex(aa => new { aa.AccessoryId, aa.AttributeKey });
        }
    }
}