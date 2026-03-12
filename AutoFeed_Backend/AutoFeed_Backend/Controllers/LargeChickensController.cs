using AutoFeed_Backend_Services.DTOs.LargeChicken;
using AutoFeed_Backend_Services.DTOs.Responses;
using AutoFeed_Backend_Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AutoFeed_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LargeChickensController : ControllerBase
    {
        private readonly ILargeChickenService _service;

        public LargeChickensController(AutoFeed_Backend_Services.Interfaces.IServiceProvider provider)
        {
            _service = provider.LargeChickenService;
        }

        // GET api/largechickens?includeInactive=false
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] bool includeInactive = false)
        {
            var data = await _service.GetAllAsync(includeInactive);
            return Ok(ApiResponse<List<LargeChickenResponseDto>>.Ok(data));
        }

        // GET api/largechickens/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound(ApiResponse<object>.Fail("Not found"));
            return Ok(ApiResponse<LargeChickenResponseDto>.Ok(item));
        }

        // GET api/largechickens/search?name=xxx&healthStatus=yyy&flockId=1&includeInactive=false
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] LargeChickenSearchDto filter)
        {
            var data = await _service.SearchAsync(filter);
            return Ok(ApiResponse<List<LargeChickenResponseDto>>.Ok(data));
        }

        // POST api/largechickens
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateLargeChickenDto dto)
        {
            if (dto == null) return BadRequest();
            var created = await _service.CreateAsync(dto);
            if (created == null) return BadRequest(ApiResponse<object>.Fail("Create failed"));
            return CreatedAtAction(nameof(Get), new { id = created.ChickenLid },
                ApiResponse<LargeChickenResponseDto>.Ok(created, "Created"));
        }

        // PUT api/largechickens
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateLargeChickenDto dto)
        {
            if (dto == null) return BadRequest();
            var ok = await _service.UpdateAsync(dto);
            if (!ok) return NotFound(ApiResponse<object>.Fail("Not found or inactive"));
            return Ok(ApiResponse<object>.Ok(null, "Updated"));
        }

        // DELETE api/largechickens/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _service.DeleteAsync(id);
            if (!ok) return NotFound(ApiResponse<object>.Fail("Not found"));
            return Ok(ApiResponse<object>.Ok(null, "Deleted"));
        }

        // PATCH api/largechickens/5/restore
        [HttpPatch("{id:int}/restore")]
        public async Task<IActionResult> Restore(int id)
        {
            var ok = await _service.RestoreAsync(id);
            if (!ok) return NotFound(ApiResponse<object>.Fail("Not found or already active"));
            return Ok(ApiResponse<object>.Ok(null, "Restored"));
        }
    }
}
