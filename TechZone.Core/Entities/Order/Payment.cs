using System;
using System.ComponentModel.DataAnnotations;

namespace TechZone.Core.Entities.Order
{
    public enum PaymentMethod
    {
        CreditCard,
        PayPal,
        CashOnDelivery
    }

    public enum PaymentStatus
    {
        Pending,
        Paid,
        Failed,
        Refunded
    }

    public class Payment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }

        [Required]
        public PaymentMethod PaymentMethod { get; set; }

        [Required]
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;

        [MaxLength(100)]
        public string TransactionId { get; set; } = string.Empty;

        public DateTime? PaidAt { get; set; }

        // Navigation
        public Order Order { get; set; }
    }
}