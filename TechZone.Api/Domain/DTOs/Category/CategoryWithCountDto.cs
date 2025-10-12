using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace TechZone.Domain.DTOs.Category
{
    public enum CategorySortBy { Id = 0, Name = 1 }

    public class CategoryWithCountDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int LaptopsCount { get; set; }
        public string CategoryImageUrl { get; set; } = string.Empty;
    }

    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string CategoryImageUrl { get; set; } = string.Empty;
    }

    public class CreateCategoryRequest
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Image file is required")]
        public IFormFile Image { get; set; } = default!;
    }

    public class UpdateCategoryRequest
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        public IFormFile? Image { get; set; }

        public bool? ClearImage { get; set; }
    }
}
