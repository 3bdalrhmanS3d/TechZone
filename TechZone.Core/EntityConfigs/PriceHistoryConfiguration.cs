using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechZone.Core.Entities;

namespace TechZone.Core.EntityConfigs
{
    public class PriceHistoryConfiguration : IEntityTypeConfiguration<PriceHistory>
    {
        public void Configure(EntityTypeBuilder<PriceHistory> builder)
        {
            builder.ToTable("PriceHistories");
            builder.HasKey(ph => ph.Id);

            builder.Property(ph => ph.ProductType)
                   .IsRequired()
                   .HasConversion<string>();

            builder.Property(ph => ph.ProductId)
                   .IsRequired();

            builder.Property(ph => ph.OldPrice)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(ph => ph.NewPrice)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(ph => ph.ChangeReason)
                   .HasMaxLength(100);

            builder.Property(ph => ph.EffectiveFrom)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(ph => ph.EffectiveTo)
                   .IsRequired(false);

            builder.Property(ph => ph.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(ph => ph.UpdatedAt)
                   .IsRequired(false);

            builder.Property(ph => ph.DeletedAt)
                   .IsRequired(false);

            builder.Property(ph => ph.IsDeleted)
                   .HasDefaultValue(false);

            builder.HasOne(ph => ph.ChangedByUser)
                   .WithMany()
                   .HasForeignKey(ph => ph.ChangedByUserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(ph => !ph.IsDeleted);

            // Indexes for better query performance
            builder.HasIndex(ph => new { ph.ProductType, ph.ProductId, ph.EffectiveFrom });
            builder.HasIndex(ph => ph.EffectiveFrom);
        }
    }
}