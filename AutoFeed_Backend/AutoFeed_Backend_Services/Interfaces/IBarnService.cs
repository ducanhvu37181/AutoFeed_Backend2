using AutoFeed_Backend_Services.DTOs.Barn;
using AutoFeed_Backend_Services.DTOs.LargeChicken;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFeed_Backend_Services.Interfaces
{
    public interface IBarnService
    {
        Task<List<BarnResponseDto>> GetAllAsync(bool status = false);
        Task<BarnResponseDto?> GetByIdAsync(int id);
        Task<List<BarnResponseDto>> SearchAsync(BarnSearchDto filter);
        Task<BarnResponseDto?> CreateAsync(CreateBarnRequestDto dto);
        Task<bool> UpdateAsync(int id, UpdateBarnDto dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> RestoreAsync(int id);
    }
}
