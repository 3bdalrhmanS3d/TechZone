using System.ComponentModel.DataAnnotations;

namespace TechZone.Core.Entities
{
    public class OrderItem : BaseEntity
    {
        public int OrderId { get; set; }

        [Required]
        public ProductType ProductType { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required, MaxLength(200)]
        public string ProductName { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string SKU { get; set; } = string.Empty;

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Range(0, double.MaxValue)]
        public decimal UnitPrice { get; set; }

        [Range(0, double.MaxValue)]
        public decimal DiscountAmount { get; set; }

        [Range(0, double.MaxValue)]
        public decimal TotalPrice { get; set; }

        // Navigation
        public Order Order { get; set; }
    }
}