using System;
using System.ComponentModel.DataAnnotations;
using TechZone.Domain.Entities;

namespace TechZone.Domain.Entities
{
    public enum ShipmentStatus
    {
        Preparing,
        Shipped,
        Delivered,
        Failed
    }

    public class Shipment : BaseEntity
    {
        public int OrderId { get; set; }

        [MaxLength(100)]
        public string Carrier { get; set; } = string.Empty;

        [MaxLength(100)]
        public string TrackingNumber { get; set; } = string.Empty;

        [Required]
        public ShipmentStatus Status { get; set; } = ShipmentStatus.Preparing;

        public DateTime? ShippedAt { get; set; }
        public DateTime? EstimatedDelivery { get; set; }
        public DateTime? DeliveredAt { get; set; }

        public string FailedReason { get; set; } = string.Empty;

        // Navigation
        public Order Order { get; set; }
    }
}