using AutoFeed_Backend_DAO.Models;
using AutoFeed_Backend_Repositories.UnitOfWork;
using AutoFeed_Backend_Services.DTOs.User;
using AutoFeed_Backend_Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFeed_Backend_Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;

        public UserService(IUnitOfWork uow) => _uow = uow;

        private static UserResponseDto ToDto(User u) => new()
        {
            UserId = u.UserId,
            RoleId = u.RoleId,
            Email = u.Email,
            FullName = u.FullName,
            Phone = u.Phone,
            Username = u.Username,
            LastLogin = u.LastLogin,
            Status = u.Status
        };

        public async Task<List<UserResponseDto>> GetAllAsync(bool includeInactive = false)
        {
            var query = _uow.Context.Users.AsQueryable();
            if (!includeInactive) query = query.Where(u => u.Status == true);
            return await query.Select(u => new UserResponseDto
            {
                UserId = u.UserId,
                RoleId = u.RoleId,
                Email = u.Email,
                FullName = u.FullName,
                Phone = u.Phone,
                Username = u.Username,
                LastLogin = u.LastLogin,
                Status = u.Status
            }).ToListAsync();
        }

        public async Task<UserResponseDto?> GetByIdAsync(int id)
        {
            var u = await _uow.Context.Users.FirstOrDefaultAsync(x => x.UserId == id);
            return u == null ? null : ToDto(u);
        }

        public async Task<List<UserResponseDto>> SearchAsync(UserSearchDto filter)
        {
            var query = _uow.Context.Users.AsQueryable();

            if (!filter.IncludeInactive)
                query = query.Where(u => u.Status == true);

            if (!string.IsNullOrWhiteSpace(filter.Keyword))
            {
                var kw = filter.Keyword.ToLower();
                query = query.Where(u =>
                    (u.FullName != null && u.FullName.ToLower().Contains(kw)) ||
                    (u.Email != null && u.Email.ToLower().Contains(kw)) ||
                    (u.Username != null && u.Username.ToLower().Contains(kw)));
            }

            if (filter.RoleId.HasValue)
                query = query.Where(u => u.RoleId == filter.RoleId.Value);

            return await query.Select(u => new UserResponseDto
            {
                UserId = u.UserId,
                RoleId = u.RoleId,
                Email = u.Email,
                FullName = u.FullName,
                Phone = u.Phone,
                Username = u.Username,
                LastLogin = u.LastLogin,
                Status = u.Status
            }).ToListAsync();
        }

        public async Task<UserResponseDto?> CreateAsync(CreateUserDto dto)
        {
            var exists = await _uow.Context.Users.AnyAsync(u =>
                u.Email == dto.Email || u.Username == dto.Username);
            if (exists) return null;

            var entity = new User
            {
                RoleId = dto.RoleId,
                Email = dto.Email,
                //Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                FullName = dto.FullName,
                Phone = dto.Phone,
                Username = dto.Username,
                Status = true
            };

            await _uow.Repository<User>().CreateAsync(entity);
            return ToDto(entity);
        }

        public async Task<bool> UpdateAsync(UpdateUserDto dto)
        {
            var entity = await _uow.Context.Users
                .FirstOrDefaultAsync(u => u.UserId == dto.UserId && u.Status == true);
            if (entity == null) return false;

            var duplicate = await _uow.Context.Users.AnyAsync(u =>
                u.UserId != dto.UserId &&
                (u.Email == dto.Email || u.Username == dto.Username));
            if (duplicate) return false;

            entity.RoleId = dto.RoleId;
            entity.Email = dto.Email;
            entity.FullName = dto.FullName;
            entity.Phone = dto.Phone;
            entity.Username = dto.Username;

            await _uow.Repository<User>().UpdateAsync(entity);
            return true;
        }

        //public async Task<bool> ChangePasswordAsync(ChangePasswordDto dto)
        //{
        //    var entity = await _uow.Context.Users
        //        .FirstOrDefaultAsync(u => u.UserId == dto.UserId && u.Status == true);
        //    if (entity == null) return false;

        //    if (!BCrypt.Net.BCrypt.Verify(dto.OldPassword, entity.Password)) return false;

        //    entity.Password = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
        //    await _uow.Repository<User>().UpdateAsync(entity);
        //    return true;
        //}

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _uow.Context.Users
                .FirstOrDefaultAsync(u => u.UserId == id && u.Status == true);
            if (entity == null) return false;

            entity.Status = false;
            await _uow.Repository<User>().UpdateAsync(entity);
            return true;
        }

        public async Task<bool> RestoreAsync(int id)
        {
            var entity = await _uow.Context.Users
                .FirstOrDefaultAsync(u => u.UserId == id && u.Status == false);
            if (entity == null) return false;

            entity.Status = true;
            await _uow.Repository<User>().UpdateAsync(entity);
            return true;
        }
    }
}
