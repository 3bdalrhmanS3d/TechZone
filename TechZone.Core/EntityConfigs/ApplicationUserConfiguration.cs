using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechZone.Core.Entities;

namespace TechZone.Core.EntityConfigs
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(u => u.FullName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(u => u.ProfileImageUrl)
                   .HasMaxLength(500);

            builder.Property(u => u.AddressLine)
                   .HasMaxLength(500);

            builder.Property(u => u.City)
                   .HasMaxLength(100);

            builder.Property(u => u.State)
                   .HasMaxLength(100);

            builder.Property(u => u.Country)
                   .HasMaxLength(100)
                   .HasDefaultValue("Egypt");

            builder.Property(u => u.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(u => u.UpdatedAt)
                   .IsRequired(false);

            builder.Property(u => u.IsDeleted)
                   .HasDefaultValue(false);

            // Configure owned type for RefreshTokens
            builder.OwnsMany(u => u.RefreshTokens, rt =>
            {
                rt.WithOwner().HasForeignKey("UserId");
                rt.Property<string>("UserId").IsRequired().HasMaxLength(450);
                rt.Property(r => r.Token).IsRequired();
                rt.Property(r => r.ExpiresOn).IsRequired();
                rt.Property(r => r.CreatedOn).IsRequired();
                rt.Property(r => r.RevokedOn).IsRequired(false);
            });
        }
    }
}