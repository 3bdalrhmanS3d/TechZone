using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechZone.Core.models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int LaptopVariantId { get; set; }
        public LaptopVariant LaptopVariant { get; set; }

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

}
