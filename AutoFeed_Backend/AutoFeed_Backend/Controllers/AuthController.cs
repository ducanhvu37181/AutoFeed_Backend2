using AutoFeed_Backend_Services.DTOs.Responses;
using AutoFeed_Backend_Services.DTOs.User;
using AutoFeed_Backend_Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AutoFeed_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(AutoFeed_Backend_Services.Interfaces.IServiceProvider provider)
        {
            _service = provider.AuthService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            if (dto == null)
                return BadRequest(ApiResponse<object>.Fail("Invalid request"));

            var result = await _service.LoginAsync(dto);

            if (result == null)
                return Unauthorized(ApiResponse<object>.Fail("Invalid email or password"));

            return Ok(ApiResponse<LoginResponseDto>.Ok(result, "Login success"));
        }
    }
}
