using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechZone.Domain.Entities;
using TechZone.Domain.Entities.Laptop;

namespace TechZone.Infrastructure.EntityConfigs
{
    public class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.ToTable("Ratings");
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Stars)
                   .IsRequired();

            builder.Property(r => r.Comment)
                   .HasColumnType("text");

            builder.Property(r => r.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("now()");

            builder.HasOne(r => r.User)
                   .WithMany(u => u.Ratings)
                   .HasForeignKey(r => r.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.Laptop)
                   .WithMany(l => l.Ratings)
                   .HasForeignKey(r => r.LaptopId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}