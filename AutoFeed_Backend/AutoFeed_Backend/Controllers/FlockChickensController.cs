using AutoFeed_Backend_Services.DTOs.FlockChicken;
using AutoFeed_Backend_Services.DTOs.Responses;
using AutoFeed_Backend_Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AutoFeed_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlockChickensController : ControllerBase
    {

        private readonly IFlockChickenService _service;

        public FlockChickensController(AutoFeed_Backend_Services.Interfaces.IServiceProvider provider)
        {
            _service = provider.FlockChickenService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] bool includeInactive = false)
        {
            var data = await _service.GetAllAsync(includeInactive);
            return Ok(ApiResponse<List<FlockChickenResponseDto>>.Ok(data));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound(ApiResponse<object>.Fail("Not found"));
            return Ok(ApiResponse<FlockChickenResponseDto>.Ok(item));
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] FlockChickenSearchDto filter)
        {
            var data = await _service.SearchAsync(filter);
            return Ok(ApiResponse<List<FlockChickenResponseDto>>.Ok(data));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFlockChickenDto dto)
        {
            if (dto == null) return BadRequest();
            var created = await _service.CreateAsync(dto);
            if (created == null) return BadRequest(ApiResponse<object>.Fail("Create failed"));
            return CreatedAtAction(nameof(Get), new { id = created.FlockId },
                ApiResponse<FlockChickenResponseDto>.Ok(created, "Created"));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateFlockChickenDto dto)
        {
            if (dto == null) return BadRequest();
            var ok = await _service.UpdateAsync(id, dto);
            if (!ok) return NotFound(ApiResponse<object>.Fail("Not found or inactive"));
            return Ok(ApiResponse<object>.Ok(null, "Updated"));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _service.DeleteAsync(id);
            if (!ok) return NotFound(ApiResponse<object>.Fail("Not found"));
            return Ok(ApiResponse<object>.Ok(null, "Deleted"));
        }

        [HttpPatch("{id:int}/restore")]
        public async Task<IActionResult> Restore(int id)
        {
            var ok = await _service.RestoreAsync(id);
            if (!ok) return NotFound(ApiResponse<object>.Fail("Not found or already active"));
            return Ok(ApiResponse<object>.Ok(null, "Restored"));
        }
    }
}
