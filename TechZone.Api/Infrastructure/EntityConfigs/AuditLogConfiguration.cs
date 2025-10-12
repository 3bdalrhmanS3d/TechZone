using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechZone.Domain.Entities;

namespace TechZone.Infrastructure.EntityConfigs
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

            builder.Property(a => a.Entity)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(a => a.EntityId)
                   .IsRequired();

            builder.Property(a => a.Timestamp)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(a => a.Details)
                   .HasColumnType("text");

            builder.HasOne(a => a.User)
                   .WithMany(u => u.AuditLogs)
                   .HasForeignKey(a => a.ApplicationUserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}