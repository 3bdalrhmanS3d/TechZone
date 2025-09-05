using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechZone.Api.Services.Interfaces;
using TechZone.Core.models;

namespace TechZone.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LaptopController : ControllerBase
    {
        private readonly ILaptopService _laptopService;

        public LaptopController(ILaptopService laptopService)
        {
            _laptopService = laptopService;
        }

        // GET: api/Laptop
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Laptop>>> GetAll()
        {
            var laptops = await _laptopService.GetAllAsync();
            return Ok(laptops);
        }

        // GET: api/Laptop/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Laptop>> GetById(int id)
        {
            var laptop = await _laptopService.GetByIdAsync(id);
            if (laptop == null)
                return NotFound();

            return Ok(laptop);
        }

        // POST: api/Laptop
        [HttpPost]
        public async Task<ActionResult<Laptop>> Create([FromBody] Laptop laptop)
        {
            if (laptop == null)
                return BadRequest();

            var createdLaptop = await _laptopService.CreateAsync(laptop);
            return CreatedAtAction(nameof(GetById), new { id = createdLaptop.Id }, createdLaptop);
        }

        // PUT: api/Laptop/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Laptop>> Update(int id, [FromBody] Laptop laptop)
        {
            if (laptop == null || id != laptop.Id)
                return BadRequest();

            var updatedLaptop = await _laptopService.UpdateAsync(id, laptop);
            if (updatedLaptop == null)
                return NotFound();

            return Ok(updatedLaptop);
        }

        // DELETE: api/Laptop/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _laptopService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
