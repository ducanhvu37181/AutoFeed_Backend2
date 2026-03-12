using AutoFeed_Backend_Services.DTOs.LargeChicken;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFeed_Backend_Services.Interfaces
{
    public interface ILargeChickenService
    {
        Task<List<LargeChickenResponseDto>> GetAllAsync(bool includeInactive = false);
        Task<LargeChickenResponseDto?> GetByIdAsync(int id);
        Task<List<LargeChickenResponseDto>> SearchAsync(LargeChickenSearchDto filter);
        Task<LargeChickenResponseDto?> CreateAsync(CreateLargeChickenDto dto);
        Task<bool> UpdateAsync(UpdateLargeChickenDto dto);
        Task<bool> DeleteAsync(int id);    // soft delete: isActive = false
        Task<bool> RestoreAsync(int id);   // isActive = true
    }
}
