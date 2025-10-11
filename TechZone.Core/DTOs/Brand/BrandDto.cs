using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechZone.Core.DTOs.Brand
{

    public enum BrandSortBy { Id = 0, Name = 1, Country = 2 }

    public class BrandDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string LogoUrl { get; set; } = string.Empty; // هنخزّن رابط الرفع هنا
        public string Description { get; set; } = string.Empty;
    }

    public class CreateBrandRequest
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Country { get; set; }

        [MaxLength(2000)]
        public string? Description { get; set; }

        // ملف اللوجو إلزامي في الإنشاء
        [Required(ErrorMessage = "Logo image is required")]
        public IFormFile Image { get; set; } = default!;
    }

    public class UpdateBrandRequest
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Country { get; set; }

        [MaxLength(2000)]
        public string? Description { get; set; }

        public IFormFile? Image { get; set; }

        public bool? ClearLogo { get; set; }
    }

    public class BrandWithCountDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int LaptopsCount { get; set; }
    }


}
