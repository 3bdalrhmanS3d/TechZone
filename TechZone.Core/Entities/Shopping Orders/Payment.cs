using System;
using System.ComponentModel.DataAnnotations;

namespace TechZone.Core.Entities
{
    public enum PaymentStatus
    {
        PENDING,
        COMPLETED,
        FAILED,
        CANCELLED,
        REFUNDED
    }

    public class Payment : BaseEntity
    {
        public int OrderId { get; set; }

        [MaxLength(100)]
        public string PaymobOrderId { get; set; } = string.Empty;

        public int? IntegrationId { get; set; }

        [MaxLength(100)]
        public string TransactionId { get; set; } = string.Empty;

        [Range(0, double.MaxValue)]
        public decimal AmountCents { get; set; }

        [MaxLength(10)]
        public string Currency { get; set; } = "EGP";

        [MaxLength(50)]
        public string PaymentMethod { get; set; } = string.Empty;

        [Required]
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.PENDING;

        [MaxLength(50)]
        public string ErrorCode { get; set; } = string.Empty;

        public string ErrorMessage { get; set; } = string.Empty;

        [Range(0, double.MaxValue)]
        public decimal RefundedAmountCents { get; set; }

        [Range(0, double.MaxValue)]
        public decimal CapturedAmountCents { get; set; }

        public bool Is3DS { get; set; } = false;
        public bool IsCaptured { get; set; } = false;
        public bool IsRefunded { get; set; } = false;
        public string BillingData { get; set; } = string.Empty;

        [MaxLength(100)]
        public string TransactionFingerprint { get; set; } = string.Empty;

        public DateTime? PaymobCreatedAt { get; set; }

        // Navigation
        public Order Order { get; set; }
    }
}