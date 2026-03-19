using AutoFeed_Backend_Services.DTOs.User;

namespace AutoFeed_Backend_Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto?> LoginAsync(LoginRequestDto dto);
    }
}
