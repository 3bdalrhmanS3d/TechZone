using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechZone.Core.models;

namespace TechZone.Core.EntityConfigs
{
    public class LaptopConfiguration : IEntityTypeConfiguration<Laptop>
    {
        public void Configure(EntityTypeBuilder<Laptop> builder)
        {
            // Table Name
            builder.ToTable("Laptops");

            // Primary Key
            builder.HasKey(l => l.Id);

            // Properties
            builder.Property(l => l.ModelName)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(l => l.Processor)
                   .HasMaxLength(100);

            builder.Property(l => l.GPU)
                   .HasMaxLength(100);

            builder.Property(l => l.ScreenSize)
                   .HasMaxLength(50);

            builder.Property(l => l.Ports)
                   .HasMaxLength(300);


            // Boolean flags
            builder.Property(l => l.HasCamera)
                   .IsRequired();

            builder.Property(l => l.HasKeyboard)
                   .IsRequired();

            builder.Property(l => l.HasTouchScreen)
                   .IsRequired();

            // Relations
            builder.HasMany(l => l.Variants)
                   .WithOne(v => v.Laptop)
                   .HasForeignKey(v => v.LaptopId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
