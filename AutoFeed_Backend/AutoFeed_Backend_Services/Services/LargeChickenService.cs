using AutoFeed_Backend_DAO.Models;
using AutoFeed_Backend_Repositories.UnitOfWork;
using AutoFeed_Backend_Services.DTOs.LargeChicken;
using AutoFeed_Backend_Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFeed_Backend_Services.Services
{
    public class LargeChickenService : ILargeChickenService
    {
        private readonly IUnitOfWork _uow;

        public LargeChickenService(IUnitOfWork uow) => _uow = uow;

        private static LargeChickenResponseDto ToDto(LargeChicken e) => new()
        {
            ChickenLid = e.ChickenLid,
            FlockId = e.FlockId,
            Name = e.Name,
            Weight = e.Weight,
            Age = e.Age,
            HealthStatus = e.HealthStatus,
            Note = e.Note,
            IsActive = e.IsActive
        };

        public async Task<List<LargeChickenResponseDto>> GetAllAsync(bool includeInactive = false)
        {
            var query = _uow.Context.LargeChickens.AsQueryable();
            if (!includeInactive) query = query.Where(x => x.IsActive);
            return await query.Select(e => new LargeChickenResponseDto
            {
                ChickenLid = e.ChickenLid,
                FlockId = e.FlockId,
                Name = e.Name,
                Weight = e.Weight,
                Age = e.Age,
                HealthStatus = e.HealthStatus,
                Note = e.Note,
                IsActive = e.IsActive
            }).ToListAsync();
        }

        public async Task<LargeChickenResponseDto?> GetByIdAsync(int id)
        {
            var entity = await _uow.Context.LargeChickens
                .FirstOrDefaultAsync(x => x.ChickenLid == id);
            return entity == null ? null : ToDto(entity);
        }

        public async Task<List<LargeChickenResponseDto>> SearchAsync(LargeChickenSearchDto filter)
        {
            var query = _uow.Context.LargeChickens.AsQueryable();

            if (!filter.IncludeInactive)
                query = query.Where(x => x.IsActive);

            if (!string.IsNullOrWhiteSpace(filter.Name))
                query = query.Where(x => x.Name.Contains(filter.Name));

            if (!string.IsNullOrWhiteSpace(filter.HealthStatus))
                query = query.Where(x => x.HealthStatus.Contains(filter.HealthStatus));

            if (filter.FlockId.HasValue)
                query = query.Where(x => x.FlockId == filter.FlockId.Value);

            return await query.Select(e => new LargeChickenResponseDto
            {
                ChickenLid = e.ChickenLid,
                FlockId = e.FlockId,
                Name = e.Name,
                Weight = e.Weight,
                Age = e.Age,
                HealthStatus = e.HealthStatus,
                Note = e.Note,
                IsActive = e.IsActive
            }).ToListAsync();
        }

        public async Task<LargeChickenResponseDto?> CreateAsync(CreateLargeChickenDto dto)
        {
            var entity = new LargeChicken
            {
                FlockId = dto.FlockId,
                Name = dto.Name,
                Weight = dto.Weight,
                Age = dto.Age,
                HealthStatus = dto.HealthStatus,
                Note = dto.Note,
                IsActive = true
            };
            await _uow.Repository<LargeChicken>().CreateAsync(entity);
            return ToDto(entity);
        }

        public async Task<bool> UpdateAsync(UpdateLargeChickenDto dto)
        {
            var entity = await _uow.Context.LargeChickens
                .FirstOrDefaultAsync(x => x.ChickenLid == dto.ChickenLid && x.IsActive);
            if (entity == null) return false;

            entity.FlockId = dto.FlockId;
            entity.Name = dto.Name;
            entity.Weight = dto.Weight;
            entity.Age = dto.Age;
            entity.HealthStatus = dto.HealthStatus;
            entity.Note = dto.Note;

            await _uow.Repository<LargeChicken>().UpdateAsync(entity);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _uow.Context.LargeChickens
                .FirstOrDefaultAsync(x => x.ChickenLid == id && x.IsActive);
            if (entity == null) return false;

            entity.IsActive = false;
            await _uow.Repository<LargeChicken>().UpdateAsync(entity);
            return true;
        }

        public async Task<bool> RestoreAsync(int id)
        {
            var entity = await _uow.Context.LargeChickens
                .FirstOrDefaultAsync(x => x.ChickenLid == id && !x.IsActive);
            if (entity == null) return false;

            entity.IsActive = true;
            await _uow.Repository<LargeChicken>().UpdateAsync(entity);
            return true;
        }
    }
}