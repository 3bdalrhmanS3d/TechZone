using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechZone.Core.DTOs.Category;
using TechZone.EF.Application;

namespace TechZone.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ApplicationDbContext context, ILogger<CategoryController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// يرجّع كل التصنيفات مع عدد اللابتوبات المرتبطة بكل تصنيف
        /// </summary>
        [HttpGet("with-laptop-counts")]
        [ProducesResponseType(typeof(List<CategoryWithCountDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategoriesWithLaptopCounts()
        {
            _logger.LogInformation("Fetching categories with laptop counts");

            // EF هيحوّل Count() لاستعلام SQL واحد فعال (LEFT JOIN) وبيضمن ظهور التصنيفات حتى لو عددها صفر
            var data = await _context.Categories
                .AsNoTracking()
                .OrderBy(c => c.Name)
                .Select(c => new CategoryWithCountDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    LaptopsCount = c.Laptops.Count() // safe & translated to SQL
                })
                .ToListAsync();

            return Ok(data);
        }

        
    }
}
