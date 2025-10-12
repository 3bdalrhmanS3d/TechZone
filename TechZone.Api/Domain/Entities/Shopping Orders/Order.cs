using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TechZone.Domain.Entities;
using TechZone.Domain.Entities.User;

namespace TechZone.Domain.Entities
{
    public enum OrderType
    {
        Reservation,
        Delivery
    }

    public enum OrderStatus
    {
        Pending,
        PaymentFailed,
        Reserved,
        Processing,
        Shipped,
        Delivered,
        Cancelled,
        Refunded,
        ReservationExpired
    }

    public class Order : BaseEntity
    {
        [Required, MaxLength(50)]
        public string OrderNumber { get; set; } = string.Empty;

        public string UserId { get; set; }

        [Required]
        public OrderType OrderType { get; set; } = OrderType.Reservation;

        // Reservation specific fields
        [Range(0, double.MaxValue)]
        public decimal ReservationAmount { get; set; }

        public DateTime? ReservationExpiryDate { get; set; }
        public bool IsReservationCompleted { get; set; } = false;

        // Delivery specific fields
        [MaxLength(500)]
        public string DeliveryAddress { get; set; } = string.Empty;

        [Range(0, double.MaxValue)]
        public decimal DeliveryCost { get; set; }

        public string DeliveryInstructions { get; set; } = string.Empty;

        // Payment amounts
        [Range(0, double.MaxValue)]
        public decimal SubtotalAmount { get; set; }

        [Range(0, double.MaxValue)]
        public decimal DiscountAmount { get; set; }

        [Range(0, double.MaxValue)]
        public decimal ShippingCost { get; set; }

        [Range(0, double.MaxValue)]
        public decimal TaxAmount { get; set; }

        [Range(0, double.MaxValue)]
        public decimal TotalAmount { get; set; }

        [Required]
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public string CancelReason { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public ApplicationUser User { get; set; }
        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public Shipment Shipment { get; set; }
        public ICollection<UserDiscountUsage> UserDiscountUsages { get; set; } = new List<UserDiscountUsage>();
    }
}