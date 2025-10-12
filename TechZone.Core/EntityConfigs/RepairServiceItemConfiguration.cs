using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechZone.Core.Entities;

namespace TechZone.Core.EntityConfigs
{
    public class RepairServiceItemConfiguration : IEntityTypeConfiguration<RepairServiceItem>
    {
        public void Configure(EntityTypeBuilder<RepairServiceItem> builder)
        {
            builder.ToTable("RepairServiceItems");
            builder.HasKey(rs => rs.Id);

            builder.Property(rs => rs.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(rs => rs.RepairType)
                   .IsRequired()
                   .HasConversion<string>();

            builder.Property(rs => rs.Description)
                   .HasColumnType("text");

            builder.Property(rs => rs.BasePrice)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(rs => rs.EstimatedDays)
                   .IsRequired();

            builder.Property(rs => rs.IsActive)
                   .HasDefaultValue(true);

            builder.Property(rs => rs.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("TIMEZONE('utc', NOW())"); // Changed from GETUTCDATE()

            builder.Property(rs => rs.UpdatedAt)
                   .IsRequired(false);

            builder.Property(rs => rs.DeletedAt)
                   .IsRequired(false);

            builder.Property(rs => rs.IsDeleted)
                   .HasDefaultValue(false);

            builder.HasQueryFilter(rs => !rs.IsDeleted);

            // Indexes
            builder.HasIndex(rs => rs.RepairType);
            builder.HasIndex(rs => rs.IsActive);
        }
    }
}