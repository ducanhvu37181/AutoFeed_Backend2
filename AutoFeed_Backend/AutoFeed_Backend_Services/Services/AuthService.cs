using AutoFeed_Backend_Repositories.UnitOfWork;
using AutoFeed_Backend_Services.DTOs.User;
using AutoFeed_Backend_Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFeed_Backend_Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _uow;

        public AuthService(IUnitOfWork uow) => _uow = uow;
        public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto dto)
        {
            var user = await _uow.Context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email && u.Password == dto.Password);

            if (user == null) return null;

            if (user.Password != dto.Password) return null;

            if (user.Status == false) return null;

            var token = Guid.NewGuid().ToString();

            return new LoginResponseDto
            {
                UserId = user.UserId,
                Email = user.Email,
                FullName = user.FullName,
                RoleId = user.RoleId ?? 0,
                Token = token
            };
        }
    }
}
