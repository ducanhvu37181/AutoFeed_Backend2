using AutoFeed_Backend_Services.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFeed_Backend_Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserResponseDto>> GetAllAsync(bool includeInactive = false);
        Task<UserResponseDto?> GetByIdAsync(int id);
        Task<List<UserResponseDto>> SearchAsync(UserSearchDto filter);
        Task<UserResponseDto?> CreateAsync(CreateUserDto dto);
        Task<bool> UpdateAsync(UpdateUserDto dto);
        Task<bool> DeleteAsync(int id);    // soft delete: status = false
        Task<bool> RestoreAsync(int id);   // status = true
    }
}
