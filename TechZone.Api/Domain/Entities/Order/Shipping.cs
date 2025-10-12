using System;
using System.ComponentModel.DataAnnotations;

namespace TechZone.Domain.Entities.Order
{
    public class Shipping
    {
        public int Id { get; set; }
        public int OrderId { get; set; }

        [Required, MaxLength(500)]
        public string Address { get; set; } = string.Empty;

        [MaxLength(100)]
        public string City { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Country { get; set; } = string.Empty;

        [MaxLength(20)]
        public string PostalCode { get; set; } = string.Empty;

        [MaxLength(100)]
        public string TrackingNumber { get; set; } = string.Empty;

        public DateTime? ShippedAt { get; set; }
        public DateTime? DeliveredAt { get; set; }

        // Navigation
        public Order Order { get; set; }
    }
}