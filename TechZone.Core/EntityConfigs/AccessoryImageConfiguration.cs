using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechZone.Core.Entities;

namespace TechZone.Core.EntityConfigs
{
    public class AccessoryImageConfiguration : IEntityTypeConfiguration<AccessoryImage>
    {
        public void Configure(EntityTypeBuilder<AccessoryImage> builder)
        {
            builder.ToTable("AccessoryImages");
            builder.HasKey(ai => ai.Id);

            builder.Property(ai => ai.ImageUrl)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(ai => ai.IsMain)
                   .HasDefaultValue(false);

            builder.Property(ai => ai.DisplayOrder)
                   .HasDefaultValue(0);

            builder.Property(ai => ai.UploadedAt)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(ai => ai.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(ai => ai.UpdatedAt)
                   .IsRequired(false);

            builder.Property(ai => ai.DeletedAt)
                   .IsRequired(false);

            builder.Property(ai => ai.IsDeleted)
                   .HasDefaultValue(false);

            builder.HasOne(ai => ai.Accessory)
                   .WithMany(a => a.Images)
                   .HasForeignKey(ai => ai.AccessoryId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasQueryFilter(ai => !ai.IsDeleted);
        }
    }
}