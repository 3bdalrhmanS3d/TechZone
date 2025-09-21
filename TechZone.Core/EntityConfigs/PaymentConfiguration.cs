using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechZone.Core.Entities;

namespace TechZone.Core.EntityConfigs
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.PaymentMethod)
                   .IsRequired()
                   .HasConversion<string>();

            builder.Property(p => p.PaymentStatus)
                   .IsRequired()
                   .HasConversion<string>()
                   .HasDefaultValue(PaymentStatus.Pending);

            builder.Property(p => p.TransactionId)
                   .HasMaxLength(100);

            builder.HasOne(p => p.Order)
                   .WithMany(o => o.Payments)
                   .HasForeignKey(p => p.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}