using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechZone.Core.Entities;
using TechZone.Core.Entities.Laptop;
using TechZone.Core.Entities.Repair;

namespace TechZone.Core.EntityConfigs
{
    public class RepairRequestConfiguration : IEntityTypeConfiguration<RepairRequest>
    {
        public void Configure(EntityTypeBuilder<RepairRequest> builder)
        {
            builder.ToTable("RepairRequests");
            builder.HasKey(rr => rr.RequestId);

            builder.Property(rr => rr.Notes)
                   .HasColumnType("text");

            builder.Property(rr => rr.Status)
                   .IsRequired()
                   .HasConversion<string>()
                   .HasDefaultValue(RepairRequestStatus.Pending);

            builder.Property(rr => rr.RequestDate)
                   .IsRequired()
                   .HasDefaultValueSql("now()");

            builder.HasOne(rr => rr.User)
                   .WithMany(u => u.RepairRequests)
                   .HasForeignKey(rr => rr.ApplicationUserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(rr => rr.RepairServiceItem)
                   .WithMany(rs => rs.RepairRequests)
                   .HasForeignKey(rr => rr.ItemId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(rr => rr.Laptop)
                   .WithMany(l => l.RepairRequests)
                   .HasForeignKey(rr => rr.LaptopId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}