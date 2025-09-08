using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechZone.Core.Entities;

namespace TechZone.Core.EntityConfigs
{
    public class LaptopVariantConfiguration : IEntityTypeConfiguration<LaptopVariant>
    {
        public void Configure(EntityTypeBuilder<LaptopVariant> builder)
        {
            // Table name
            builder.ToTable("LaptopVariants");

            // Primary Key
            builder.HasKey(v => v.Id);

            // Properties
            builder.Property(v => v.RAM)
                   .IsRequired();

            builder.Property(v => v.Storage)
                   .IsRequired();


            builder.Property(v => v.StockQuantity)
                   .IsRequired();

            builder.Property(x => x.Price)
                   .HasColumnType("decimal(18,2)");


            // Relations
            builder.HasOne(v => v.Laptop)
                   .WithMany(l => l.Variants)
                   .HasForeignKey(v => v.LaptopId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
