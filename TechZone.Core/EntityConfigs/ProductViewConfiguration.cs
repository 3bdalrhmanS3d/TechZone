using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechZone.Core.Entities;

namespace TechZone.Core.EntityConfigs
{
    public class ProductViewConfiguration : IEntityTypeConfiguration<ProductView>
    {
        public void Configure(EntityTypeBuilder<ProductView> builder)
        {
            builder.ToTable("ProductViews");
            builder.HasKey(pv => pv.Id);

            builder.Property(pv => pv.ProductType)
                   .IsRequired()
                   .HasConversion<string>();

            builder.Property(pv => pv.ProductId)
                   .IsRequired();

            builder.Property(pv => pv.IpAddress)
                   .HasMaxLength(50);

            builder.Property(pv => pv.ViewedAt)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(pv => pv.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(pv => pv.UpdatedAt)
                   .IsRequired(false);

            builder.Property(pv => pv.DeletedAt)
                   .IsRequired(false);

            builder.Property(pv => pv.IsDeleted)
                   .HasDefaultValue(false);

            builder.HasOne(pv => pv.User)
                   .WithMany(u => u.ProductViews)
                   .HasForeignKey(pv => pv.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(pv => !pv.IsDeleted);

            // Indexes
            builder.HasIndex(pv => new { pv.ProductType, pv.ProductId, pv.ViewedAt });
            builder.HasIndex(pv => pv.UserId);
        }
    }
}