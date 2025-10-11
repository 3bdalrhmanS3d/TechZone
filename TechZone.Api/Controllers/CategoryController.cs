using Microsoft.AspNetCore.Mvc;
using TechZone.Core.DTOs;
using TechZone.Core.DTOs.Category;
using TechZone.Core.PagedResult;
using TechZone.Core.Service.Interfaces;
using TechZone.Core.ServiceResponse;

namespace TechZone.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryService service, ILogger<CategoryController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ServiceResponse<PagedResult<CategoryDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] PaginationParamsDto<CategorySortBy> request, CancellationToken ct)
        {
            var resp = await _service.GetAllAsync(request, ct);
            return StatusCode(resp.StatusCode, resp);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ServiceResponse<CategoryDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(int id, CancellationToken ct)
        {
            var resp = await _service.GetByIdAsync(id, ct);
            return StatusCode(resp.StatusCode, resp);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(ServiceResponse<CategoryDto>), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromForm] CreateCategoryRequest dto, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(kv => kv.Value?.Errors?.Count > 0)
                    .ToDictionary(kv => kv.Key, kv => kv.Value!.Errors.Select(e => e.ErrorMessage).ToList());

                var bad = ServiceResponse<CategoryDto>.ValidationErrorResponse(errors);
                return StatusCode(bad.StatusCode, bad);
            }

            var resp = await _service.CreateAsync(dto, ct);
            return StatusCode(resp.StatusCode, resp);
        }

        [HttpPut("{id:int}")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(ServiceResponse<object>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateCategoryRequest dto, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(kv => kv.Value?.Errors?.Count > 0)
                    .ToDictionary(kv => kv.Key, kv => kv.Value!.Errors.Select(e => e.ErrorMessage).ToList());

                var bad = ServiceResponse<object>.ValidationErrorResponse(errors);
                return StatusCode(bad.StatusCode, bad);
            }

            var resp = await _service.UpdateAsync(id, dto, ct);
            return StatusCode(resp.StatusCode, resp);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(ServiceResponse<object>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            var resp = await _service.DeleteAsync(id, ct);
            return StatusCode(resp.StatusCode, resp);
        }

        [HttpGet("with-laptop-counts")]
        [ProducesResponseType(typeof(ServiceResponse<List<CategoryWithCountDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategoriesWithLaptopCounts(CancellationToken ct)
        {
            var resp = await _service.GetWithLaptopCountsAsync(ct);
            return StatusCode(resp.StatusCode, resp);
        }
    }
}
