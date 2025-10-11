using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechZone.Core.Entities;
using TechZone.Core.Entities.Logging;

namespace TechZone.Core.EntityConfigs
{
    public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.ToTable("AuditLogs");
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Action)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(a => a.EntityType)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(a => a.EntityId)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(a => a.OldValues)
                   .HasColumnType("text");

            builder.Property(a => a.NewValues)
                   .HasColumnType("text");

            builder.Property(a => a.IpAddress)
                   .HasMaxLength(50);

            builder.Property(a => a.UserAgent)
                   .HasMaxLength(500);

            builder.Property(a => a.Timestamp)
                   .IsRequired()
                   .HasDefaultValueSql("now()");

            builder.Property(a => a.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(a => a.UpdatedAt)
                   .IsRequired(false);

            builder.Property(a => a.DeletedAt)
                   .IsRequired(false);

            builder.Property(a => a.IsDeleted)
                   .HasDefaultValue(false);

            builder.HasOne(a => a.User)
                   .WithMany(u => u.AuditLogs)
                   .HasForeignKey(a => a.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(a => !a.IsDeleted);

            // Indexes
            builder.HasIndex(a => a.UserId);
            builder.HasIndex(a => new { a.EntityType, a.EntityId });
            builder.HasIndex(a => a.Timestamp);
        }
    }
}