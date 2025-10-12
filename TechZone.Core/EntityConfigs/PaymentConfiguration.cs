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

            builder.Property(p => p.PaymobOrderId)
                   .HasMaxLength(100);

            builder.Property(p => p.IntegrationId)
                   .IsRequired(false);

            builder.Property(p => p.TransactionId)
                   .HasMaxLength(100);

            builder.Property(p => p.AmountCents)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Currency)
                   .HasMaxLength(10)
                   .HasDefaultValue("EGP");

            builder.Property(p => p.PaymentMethod)
                   .HasMaxLength(50);

            builder.Property(p => p.PaymentStatus)
                   .IsRequired()
                   .HasConversion<string>()
                   .HasDefaultValue(PaymentStatus.PENDING);

            builder.Property(p => p.ErrorCode)
                   .HasMaxLength(50);

            builder.Property(p => p.ErrorMessage)
                   .HasColumnType("text");

            builder.Property(p => p.RefundedAmountCents)
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0);

            builder.Property(p => p.CapturedAmountCents)
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0);

            builder.Property(p => p.Is3DS)
                   .HasDefaultValue(false);

            builder.Property(p => p.IsCaptured)
                   .HasDefaultValue(false);

            builder.Property(p => p.IsRefunded)
                   .HasDefaultValue(false);

            builder.Property(p => p.BillingData)
                   .HasColumnType("text");

            builder.Property(p => p.TransactionFingerprint)
                   .HasMaxLength(100);

            builder.Property(p => p.PaymobCreatedAt)
                   .IsRequired(false);

            builder.Property(p => p.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("TIMEZONE('utc', NOW())"); // Changed from GETUTCDATE()





            builder.Property(p => p.UpdatedAt)
                   .IsRequired(false);

            builder.Property(p => p.DeletedAt)
                   .IsRequired(false);

            builder.Property(p => p.IsDeleted)
                   .HasDefaultValue(false);

            builder.HasOne(p => p.Order)
                   .WithMany(o => o.Payments)
                   .HasForeignKey(p => p.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasQueryFilter(p => !p.IsDeleted);

            // Indexes
            builder.HasIndex(p => p.OrderId);
            builder.HasIndex(p => p.PaymobOrderId).IsUnique();
            builder.HasIndex(p => p.TransactionId).IsUnique();
            builder.HasIndex(p => p.PaymentStatus);
        }
    }
}