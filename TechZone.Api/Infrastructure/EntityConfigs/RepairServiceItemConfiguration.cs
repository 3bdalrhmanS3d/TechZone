using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechZone.Domain.Entities;

namespace TechZone.Infrastructure.EntityConfigs

{
    public class RepairServiceItemConfiguration : IEntityTypeConfiguration<RepairServiceItem>
    {
        public void Configure(EntityTypeBuilder<RepairServiceItem> builder)
        {
            builder.ToTable("RepairServiceItems");
            builder.HasKey(rs => rs.ItemId);

            builder.Property(rs => rs.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(rs => rs.RepairType)
                   .IsRequired()
                   .HasConversion<string>();

            builder.Property(rs => rs.Price)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(rs => rs.EstimatedTime)
                   .HasMaxLength(100);
        }
    }
}