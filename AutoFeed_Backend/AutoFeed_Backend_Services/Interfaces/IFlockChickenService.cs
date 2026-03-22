using AutoFeed_Backend_Services.DTOs.FlockChicken;

namespace AutoFeed_Backend_Services.Interfaces
{
    public interface IFlockChickenService
    {
        Task<List<FlockChickenResponseDto>> GetAllAsync(bool status = false);
        Task<FlockChickenResponseDto?> GetByIdAsync(int id);
        Task<List<FlockChickenResponseDto>> SearchAsync(FlockChickenSearchDto filter);
        Task<FlockChickenResponseDto?> CreateAsync(CreateFlockChickenDto dto);
        Task<bool> UpdateAsync(int id, UpdateFlockChickenDto dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> RestoreAsync(int id);
    }
}
