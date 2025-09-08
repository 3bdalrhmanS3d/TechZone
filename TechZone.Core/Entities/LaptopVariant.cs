using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechZone.Core.Entities
{
    public class LaptopVariant
    {
        public int Id { get; set; }
        public int LaptopId { get; set; }
        public Laptop Laptop { get; set; }

        public int RAM { get; set; }            // بالـ GB
        public int Storage { get; set; }        // بالـ GB أو TB
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }

}
