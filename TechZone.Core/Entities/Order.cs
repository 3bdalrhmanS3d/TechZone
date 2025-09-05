using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechZone.Core.models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }      // Pending, Paid, Shipped, Completed
        public string UserId { get; set; }

        // Navigation
        public ApplicationUser User { get; set; }
        public ICollection<OrderItem> Items { get; set; }
    }

}
