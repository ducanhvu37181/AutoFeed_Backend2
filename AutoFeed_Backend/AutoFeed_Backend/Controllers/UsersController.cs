using AutoFeed_Backend_Services.DTOs.Responses;
using AutoFeed_Backend_Services.DTOs.User;
using AutoFeed_Backend_Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AutoFeed_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(AutoFeed_Backend_Services.Interfaces.IServiceProvider provider)
        {
            _service = provider.UserService;
        }

        // GET api/users?includeInactive=false
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] bool includeInactive = false)
        {
            var data = await _service.GetAllAsync(includeInactive);
            return Ok(ApiResponse<List<UserResponseDto>>.Ok(data));
        }

        // GET api/users/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound(ApiResponse<object>.Fail("Not found"));
            return Ok(ApiResponse<UserResponseDto>.Ok(item));
        }

        // GET api/users/search?keyword=john&roleId=2&includeInactive=false
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] UserSearchDto filter)
        {
            var data = await _service.SearchAsync(filter);
            return Ok(ApiResponse<List<UserResponseDto>>.Ok(data));
        }

        // POST api/users
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
        {
            if (dto == null) return BadRequest();
            var created = await _service.CreateAsync(dto);
            if (created == null)
                return Conflict(ApiResponse<object>.Fail("Email or username already exists"));
            return CreatedAtAction(nameof(Get), new { id = created.UserId },
                ApiResponse<UserResponseDto>.Ok(created, "Created"));
        }

        // PUT api/users
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUserDto dto)
        {
            if (dto == null) return BadRequest();
            var ok = await _service.UpdateAsync(dto);
            if (!ok) return Conflict(ApiResponse<object>.Fail("Not found or duplicate email/username"));
            return Ok(ApiResponse<object>.Ok(null, "Updated"));
        }


        // DELETE api/users/5  (soft delete — status = false)
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _service.DeleteAsync(id);
            if (!ok) return NotFound(ApiResponse<object>.Fail("Not found"));
            return Ok(ApiResponse<object>.Ok(null, "Deleted"));
        }

        // PATCH api/users/5/restore
        [HttpPatch("{id:int}/restore")]
        public async Task<IActionResult> Restore(int id)
        {
            var ok = await _service.RestoreAsync(id);
            if (!ok) return NotFound(ApiResponse<object>.Fail("Not found or already active"));
            return Ok(ApiResponse<object>.Ok(null, "Restored"));
        }
    }
}
