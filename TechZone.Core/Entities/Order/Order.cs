using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TechZone.Core.Entities;

namespace TechZone.Core.Entities
{
    public enum OrderStatus
    {
        Pending,
        Paid,
        Shipped,
        Completed
    }

    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        [Range(0, double.MaxValue)]
        public decimal TotalAmount { get; set; }

        [Required]
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        [ForeignKey(nameof(ApplicationUser))]
        public string UserId { get; set; } 

        // Navigation property
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public Shipping Shipping { get; set; }
    }
}