using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechZone.Core.DTOs.Category
{
    public class CategoryWithCountDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int LaptopsCount { get; set; }
    }

}
