using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechZone.Domain.DTOs.Laptop
{
    public class LaptopResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string Category { get; set; } = null!;
        public List<string> Images { get; set; } = new List<string>();

        public double Rate { get; set; }

        public int ReviewsCount { get; set; } = 0;

        public bool IsDiscounted { get; set; } = false;

        public decimal? DiscountedPrice { get; set; } = null;

        public string ShortDescription { get; set; } = null!;

        // New properties for enhanced response
        public string Brand { get; set; } = null!;
        public string Processor { get; set; } = null!;
        public string GPU { get; set; } = null!;
        public string ScreenSize { get; set; } = null!;
        public string PriceRange { get; set; } = null!;
    }
}