using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TechZone.Api.DTOs.Laptop;
using TechZone.Api.Extensions;
using TechZone.Api.Services.Interfaces;
using TechZone.Core.DTOs.Laptop;
using TechZone.Core.Entities;
using TechZone.Core.ENUMS.Laptop;
using TechZone.Core.PagedResult;
using TechZone.Core.ServiceResponse;

namespace TechZone.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LaptopController : ControllerBase
    {
        private readonly ILaptopService _laptopService;
        private readonly ILogger<LaptopController> _logger;

        public LaptopController(ILaptopService laptopService, ILogger<LaptopController> logger)
        {
            _laptopService = laptopService;
            _logger = logger;
        }

        /// <summary>
        /// Get paginated, filtered, and sorted laptops
        /// </summary>
        /// <param name="paginationParams">Pagination, filtering, and sorting parameters</param>
        /// <returns>Paginated list of laptops with their variants</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ServiceResponse<PagedResult<LaptopResponseDTO>>), 200)]
        [ProducesResponseType(typeof(ServiceResponse<PagedResult<LaptopResponseDTO>>), 400)]
        [ProducesResponseType(typeof(ServiceResponse<PagedResult<LaptopResponseDTO>>), 500)]
        [AllowAnonymous]
        public async Task<ActionResult<ServiceResponse<PagedResult<LaptopResponseDTO>>>> GetAll(
            [FromQuery] PaginationParamsDto<LaptopSortBy> paginationParams)
        {
            _logger.LogInformation("GET api/laptop - Retrieving laptops with pagination parameters: {@PaginationParams}", paginationParams);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _laptopService.GetAllAsync(paginationParams);
            return response.ToActionResult();
        }


        /// <summary>
        /// Get paginated, filtered, and sorted laptops
        /// </summary>
        /// <param name="paginationParams">Pagination, filtering, and sorting parameters</param>
        /// <returns>Paginated list of laptops with their variants</returns>
        [HttpGet("recommended")]
        [ProducesResponseType(typeof(ServiceResponse<PagedResult<LaptopResponseDTO>>), 200)]
        [ProducesResponseType(typeof(ServiceResponse<PagedResult<LaptopResponseDTO>>), 400)]
        [ProducesResponseType(typeof(ServiceResponse<PagedResult<LaptopResponseDTO>>), 500)]
        public async Task<ActionResult<ServiceResponse<PagedResult<LaptopResponseDTO>>>> GetRecommended(
            [FromQuery] PaginationParamsDto<LaptopSortBy> paginationParams)
        {
            _logger.LogInformation("GET api/Recommended laptops - Retrieving laptops with pagination parameters: {@PaginationParams}", paginationParams);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _laptopService.GetAllAsync(paginationParams);
            return response.ToActionResult();
        }

        /// <summary>
        /// Get laptop by ID
        /// </summary>
        /// <param name="id">Laptop ID</param>
        /// <returns>Laptop details with variants</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ServiceResponse<Laptop>), 200)]
        [ProducesResponseType(typeof(ServiceResponse<Laptop>), 400)]
        [ProducesResponseType(typeof(ServiceResponse<Laptop>), 404)]
        [ProducesResponseType(typeof(ServiceResponse<Laptop>), 500)]
        public async Task<ActionResult<ServiceResponse<Laptop>>> GetById(int id)
        {
            _logger.LogInformation("GET api/laptop/{LaptopId} - Retrieving laptop by ID", id);

            var response = await _laptopService.GetByIdAsync(id);
            return response.ToActionResult();
        }

        /// <summary>
        /// Create a new laptop
        /// </summary>
        /// <param name="laptop">Laptop details</param>
        /// <returns>Created laptop</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ServiceResponse<CreateLaptopDto>), 201)]
        [ProducesResponseType(typeof(ServiceResponse<object>), 400)]
        [ProducesResponseType(typeof(ServiceResponse<object>), 409)]
        [ProducesResponseType(typeof(ServiceResponse<object>), 500)]
        public async Task<ActionResult<ServiceResponse<Laptop>>> Create([FromBody] CreateLaptopDto laptop)
        {
            _logger.LogInformation("POST api/laptop - Creating new laptop");

            if (!ModelState.IsValid)
            {
                var validationResponse = ModelState.ToValidationErrorResponse<Laptop>();
                return validationResponse.ToActionResult();
            }

            var response = await _laptopService.CreateAsync(laptop);

            if (response.IsSuccessful())
            {
                return response.ToCreatedActionResult(nameof(GetById), new { id = response.Data?.Id });
            }

            return response.ToActionResult();
        }

        /// <summary>
        /// Update an existing laptop
        /// </summary>
        /// <param name="id">Laptop ID</param>
        /// <param name="laptop">Updated laptop details</param>
        /// <returns>Updated laptop</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ServiceResponse<Laptop>), 200)]
        [ProducesResponseType(typeof(ServiceResponse<Laptop>), 400)]
        [ProducesResponseType(typeof(ServiceResponse<Laptop>), 404)]
        [ProducesResponseType(typeof(ServiceResponse<Laptop>), 409)]
        [ProducesResponseType(typeof(ServiceResponse<Laptop>), 500)]
        public async Task<ActionResult<ServiceResponse<Laptop>>> Update(int id, [FromBody] UpdateLaptopDto laptop)
        {
            _logger.LogInformation("PUT api/laptop/{LaptopId} - Updating laptop", id);

            if (!ModelState.IsValid)
            {
                var validationResponse = ModelState.ToValidationErrorResponse<Laptop>();
                return validationResponse.ToActionResult();
            }

            var response = await _laptopService.UpdateAsync(id, laptop);
            return response.ToActionResult();
        }

        /// <summary>
        /// Delete a laptop
        /// </summary>
        /// <param name="id">Laptop ID</param>
        /// <returns>Deletion result</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ServiceResponse<bool>), 200)]
        [ProducesResponseType(typeof(ServiceResponse<bool>), 400)]
        [ProducesResponseType(typeof(ServiceResponse<bool>), 404)]
        [ProducesResponseType(typeof(ServiceResponse<bool>), 500)]
        public async Task<ActionResult<ServiceResponse<bool>>> Delete(int id)
        {
            _logger.LogInformation("DELETE api/laptop/{LaptopId} - Deleting laptop", id);

            var response = await _laptopService.DeleteAsync(id);
            return response.ToActionResult();
        }

        /// <summary>
        /// Search laptops by term
        /// </summary>
        /// <param name="searchTerm">Search term for model name, processor, or GPU</param>
        /// <returns>List of matching laptops</returns>
        [HttpGet("search")]
        [ProducesResponseType(typeof(ServiceResponse<IEnumerable<Laptop>>), 200)]
        [ProducesResponseType(typeof(ServiceResponse<IEnumerable<Laptop>>), 400)]
        [ProducesResponseType(typeof(ServiceResponse<IEnumerable<Laptop>>), 500)]
        [AllowAnonymous]
        public async Task<ActionResult<ServiceResponse<IEnumerable<Laptop>>>> Search([FromQuery] string searchTerm)
        {
            _logger.LogInformation("GET api/laptop/search - Searching laptops with term: {SearchTerm}", searchTerm);

            var response = await _laptopService.SearchAsync(searchTerm);
            return response.ToActionResult();
        }

        /// <summary>
        /// Filter laptops by specifications
        /// </summary>
        /// <param name="processor">Processor filter</param>
        /// <param name="gpu">GPU filter</param>
        /// <param name="minPrice">Minimum price filter</param>
        /// <param name="maxPrice">Maximum price filter</param>
        /// <returns>List of filtered laptops</returns>
        [HttpGet("filter")]
        [ProducesResponseType(typeof(ServiceResponse<IEnumerable<Laptop>>), 200)]
        [ProducesResponseType(typeof(ServiceResponse<IEnumerable<Laptop>>), 500)]
        public async Task<ActionResult<ServiceResponse<IEnumerable<Laptop>>>> FilterBySpecs(
            [FromQuery] string? processor = null,
            [FromQuery] string? gpu = null,
            [FromQuery] int? minPrice = null,
            [FromQuery] int? maxPrice = null)
        {
            _logger.LogInformation("GET api/laptop/filter - Filtering laptops by specifications");

            var response = await _laptopService.GetBySpecificationsAsync(processor, gpu, minPrice, maxPrice);
            return response.ToActionResult();
        }

    }
}