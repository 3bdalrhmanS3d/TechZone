using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechZone.Core.Entities;

namespace TechZone.Core.EntityConfigs
{
    public class RepairRequestConfiguration : IEntityTypeConfiguration<RepairRequest>
    {
        public void Configure(EntityTypeBuilder<RepairRequest> builder)
        {
            builder.ToTable("RepairRequests");
            builder.HasKey(rr => rr.Id);

            builder.Property(rr => rr.RequestNumber)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(rr => rr.DeviceSerial)
                   .HasMaxLength(100);

            builder.Property(rr => rr.IssueDescription)
                   .IsRequired()
                   .HasColumnType("text");

            builder.Property(rr => rr.DiagnosisNotes)
                   .HasColumnType("text");

            builder.Property(rr => rr.QuotedPrice)
                   .HasColumnType("decimal(18,2)");

            builder.Property(rr => rr.Status)
                   .IsRequired()
                   .HasConversion<string>()
                   .HasDefaultValue(RepairRequestStatus.Pending);

            builder.Property(rr => rr.Priority)
                   .IsRequired()
                   .HasConversion<string>()
                   .HasDefaultValue(RepairPriority.Normal);

            builder.Property(rr => rr.RequestDate)
                   .IsRequired()
                   .HasDefaultValueSql("TIMEZONE('utc', NOW())"); // Changed from GETUTCDATE()



            builder.Property(rr => rr.CompletedDate)
                   .IsRequired(false);

            builder.Property(rr => rr.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("TIMEZONE('utc', NOW())"); // Changed from GETUTCDATE()

            builder.Property(rr => rr.UpdatedAt)
                   .IsRequired(false);

            builder.Property(rr => rr.DeletedAt)
                   .IsRequired(false);

            builder.Property(rr => rr.IsDeleted)
                   .HasDefaultValue(false);

            builder.HasOne(rr => rr.User)
                   .WithMany(u => u.RepairRequests)
                   .HasForeignKey(rr => rr.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(rr => rr.ServiceItem)
                   .WithMany(si => si.RepairRequests)
                   .HasForeignKey(rr => rr.ServiceItemId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(rr => rr.LaptopVariant)
                   .WithMany(lv => lv.RepairRequests)
                   .HasForeignKey(rr => rr.LaptopVariantId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasQueryFilter(rr => !rr.IsDeleted);

            // Indexes
            builder.HasIndex(rr => rr.RequestNumber).IsUnique();
            builder.HasIndex(rr => rr.UserId);
            builder.HasIndex(rr => rr.Status);
            builder.HasIndex(rr => rr.RequestDate);
        }
    }
}