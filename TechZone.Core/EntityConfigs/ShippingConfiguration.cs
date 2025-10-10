using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechZone.Core.Entities;
using TechZone.Core.Entities.Order;

namespace TechZone.Core.EntityConfigs
{
    public class ShippingConfiguration : IEntityTypeConfiguration<Shipping>
    {
        public void Configure(EntityTypeBuilder<Shipping> builder)
        {
            builder.ToTable("Shippings");
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Address)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(s => s.City)
                   .HasMaxLength(100);

            builder.Property(s => s.Country)
                   .HasMaxLength(100);

            builder.Property(s => s.PostalCode)
                   .HasMaxLength(20);

            builder.Property(s => s.TrackingNumber)
                   .HasMaxLength(100);

            builder.HasOne(s => s.Order)
                   .WithOne(o => o.Shipping)
                   .HasForeignKey<Shipping>(s => s.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}