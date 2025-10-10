using System.ComponentModel.DataAnnotations;

namespace TechZone.Core.Entities
{
    public class ProductDiscount : BaseEntity
    {
        public int DiscountId { get; set; }

        [Required]
        public ProductType ProductType { get; set; }

        [Required]
        public int ProductId { get; set; }

        // Navigation
        public Discount Discount { get; set; }
    }
}