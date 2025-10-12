using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechZone.Core.Entities;

namespace TechZone.Core.EntityConfigs
{
    public class LaptopPortConfiguration : IEntityTypeConfiguration<LaptopPort>
    {
        public void Configure(EntityTypeBuilder<LaptopPort> builder)
        {
            builder.ToTable("LaptopPorts");
            builder.HasKey(lp => lp.Id);

            builder.Property(lp => lp.PortType)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(lp => lp.Quantity)
                   .HasDefaultValue(1);

            builder.Property(lp => lp.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("TIMEZONE('utc', NOW())"); // Changed from GETUTCDATE()


            builder.Property(lp => lp.UpdatedAt)
                   .IsRequired(false);

            builder.Property(lp => lp.DeletedAt)
                   .IsRequired(false);

            builder.Property(lp => lp.IsDeleted)
                   .HasDefaultValue(false);

            builder.HasOne(lp => lp.Laptop)
                   .WithMany(l => l.Ports)
                   .HasForeignKey(lp => lp.LaptopId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasQueryFilter(lp => !lp.IsDeleted);
        }
    }
}