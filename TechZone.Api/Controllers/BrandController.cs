using Microsoft.AspNetCore.Mvc;
using TechZone.Domain.DTOs.Brand;
using TechZone.Domain.PagedResult;
using TechZone.Domain.Service.Interfaces;
using TechZone.Domain.ServiceResponse;

namespace TechZone.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _service;
        private readonly ILogger<BrandController> _logger;

        public BrandController(IBrandService service, ILogger<BrandController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ServiceResponse<PagedResult<BrandDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] PaginationParamsDto<BrandSortBy> request, CancellationToken ct)
        {
            var resp = await _service.GetAllAsync(request, ct);
            return StatusCode(resp.StatusCode, resp);
        }


        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ServiceResponse<BrandDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(int id, CancellationToken ct)
        {
            var resp = await _service.GetByIdAsync(id, ct);
            return StatusCode(resp.StatusCode, resp);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(ServiceResponse<BrandDto>), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromForm] CreateBrandRequest dto, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(kv => kv.Value?.Errors?.Count > 0)
                    .ToDictionary(kv => kv.Key, kv => kv.Value!.Errors.Select(e => e.ErrorMessage).ToList());
                var bad = ServiceResponse<BrandDto>.ValidationErrorResponse(errors);
                return StatusCode(bad.StatusCode, bad);
            }

            var resp = await _service.CreateAsync(dto, ct);
            return StatusCode(resp.StatusCode, resp);
        }

        [HttpPut("{id:int}")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(ServiceResponse<object>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromForm] UpdateBrandRequest dto, CancellationToken ct)
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
        [ProducesResponseType(typeof(ServiceResponse<List<BrandWithCountDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBrandsWithLaptopCounts(CancellationToken ct)
        {
            var resp = await _service.GetWithLaptopCountsAsync(ct);
            return StatusCode(resp.StatusCode, resp);
        }
    }
}
