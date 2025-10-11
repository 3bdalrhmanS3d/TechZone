using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechZone.Core.Entities;

namespace TechZone.Core.EntityConfigs
{
    public class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.ToTable("Ratings");
            builder.HasKey(r => r.Id);

            builder.Property(r => r.ProductType)
                   .IsRequired()
                   .HasConversion<string>();

            builder.Property(r => r.ProductId)
                   .IsRequired();

            builder.Property(r => r.Stars)
                   .IsRequired();

            builder.Property(r => r.Comment)
                   .HasColumnType("text");

            builder.Property(r => r.IsVerifiedPurchase)
                   .HasDefaultValue(false);

            builder.Property(r => r.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(r => r.UpdatedAt)
                   .IsRequired(false);

            builder.Property(r => r.DeletedAt)
                   .IsRequired(false);

            builder.Property(r => r.IsDeleted)
                   .HasDefaultValue(false);

            builder.HasOne(r => r.User)
                   .WithMany(u => u.Ratings)
                   .HasForeignKey(r => r.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(r => !r.IsDeleted);

            // Unique constraint - user can only rate a product once
            builder.HasIndex(r => new { r.UserId, r.ProductType, r.ProductId })
                   .IsUnique();

            // Indexes for better query performance
            builder.HasIndex(r => new { r.ProductType, r.ProductId });
            builder.HasIndex(r => r.Stars);
        }
    }
}