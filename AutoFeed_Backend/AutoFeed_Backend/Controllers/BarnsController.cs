using AutoFeed_Backend_Services.DTOs.Barn;
using AutoFeed_Backend_Services.DTOs.Responses;
using AutoFeed_Backend_Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AutoFeed_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BarnsController : ControllerBase
    {
        private readonly IBarnService _service;

        public BarnsController(AutoFeed_Backend_Services.Interfaces.IServiceProvider provider)
        {
            _service = provider.BarnService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] bool includeInactive = false)
        {
            var data = await _service.GetAllAsync(includeInactive);
            return Ok(ApiResponse<List<BarnResponseDto>>.Ok(data));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound(ApiResponse<object>.Fail("Not found"));
            return Ok(ApiResponse<BarnResponseDto>.Ok(item));
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] BarnSearchDto filter)
        {
            var data = await _service.SearchAsync(filter);
            return Ok(ApiResponse<List<BarnResponseDto>>.Ok(data));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBarnRequestDto dto)
        {
            if (dto == null) return BadRequest();
            var created = await _service.CreateAsync(dto);
            if (created == null) return BadRequest(ApiResponse<object>.Fail("Create failed"));
            return CreatedAtAction(nameof(Get), new { id = created.BarnId },
                ApiResponse<BarnResponseDto>.Ok(created, "Created"));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateBarnDto dto)
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
