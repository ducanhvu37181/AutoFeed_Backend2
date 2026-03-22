using AutoFeed_Backend_DAO.Models;
using AutoFeed_Backend_Repositories.UnitOfWork;
using AutoFeed_Backend_Services.DTOs.FlockChicken;
using AutoFeed_Backend_Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AutoFeed_Backend_Services.Services
{
    public class FlockChickenService : IFlockChickenService
    {
        private readonly IUnitOfWork _uow;

        public FlockChickenService(IUnitOfWork uow) => _uow = uow;

        private static FlockChickenResponseDto ToDto(FlockChicken e) => new()
        {
            FlockId = e.FlockId,
            Name = e.Name,
            Quantity = e.Quantity,
            Weight = e.Weight,
            Age = e.Age,
            HealthStatus = e.HealthStatus,
            Note = e.Note
        };

        public async Task<List<FlockChickenResponseDto>> GetAllAsync(bool status = false)
        {
            var query = _uow.Context.FlockChickens.AsQueryable();
            if (!status) query = query.Where(x => x.HealthStatus != null);
            return await query.Select(e => new FlockChickenResponseDto
            {
                FlockId = e.FlockId,
                Name = e.Name,
                Quantity = e.Quantity,
                Weight = e.Weight,
                Age = e.Age,
                HealthStatus = e.HealthStatus,
                Note = e.Note
            }).ToListAsync();
        }

        public async Task<FlockChickenResponseDto?> GetByIdAsync(int id)
        {
            var entity = await _uow.Context.FlockChickens
                .FirstOrDefaultAsync(x => x.FlockId == id);
            return entity == null ? null : ToDto(entity);
        }

        public async Task<List<FlockChickenResponseDto>> SearchAsync(FlockChickenSearchDto filter)
        {
            var query = _uow.Context.FlockChickens.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.HealthStatus))
                query = query.Where(x => x.HealthStatus.Contains(filter.HealthStatus));

            if (!string.IsNullOrWhiteSpace(filter.Name))
                query = query.Where(x => x.Name.Contains(filter.Name));

            return await query.Select(e => new FlockChickenResponseDto
            {
                FlockId = e.FlockId,
                Name = e.Name,
                Quantity = e.Quantity,
                Weight = e.Weight,
                Age = e.Age,
                HealthStatus = e.HealthStatus,
                Note = e.Note
            }).ToListAsync();
        }

        public async Task<FlockChickenResponseDto?> CreateAsync(CreateFlockChickenDto dto)
        {
            var entity = new FlockChicken
            {
                Name = dto.Name,
                Quantity = dto.Quantity,
                Weight = dto.Weight,
                Age = dto.Age,
                HealthStatus = dto.HealthStatus,
                Note = dto.Note
            };
            await _uow.Repository<FlockChicken>().CreateAsync(entity);
            return ToDto(entity);
        }

        public async Task<bool> UpdateAsync(int id, UpdateFlockChickenDto dto)
        {
            var entity = await _uow.Context.FlockChickens
                .FirstOrDefaultAsync(x => x.FlockId == id && x.HealthStatus != null);
            if (entity == null) return false;

            entity.Name = dto.Name;
            entity.Quantity = dto.Quantity;
            entity.Weight = dto.Weight;
            entity.Age = dto.Age;
            entity.HealthStatus = dto.HealthStatus;
            entity.Note = dto.Note;

            await _uow.Repository<FlockChicken>().UpdateAsync(entity);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _uow.Context.FlockChickens
                .FirstOrDefaultAsync(x => x.FlockId == id && x.HealthStatus!= null);
            if (entity == null) return false;

            entity.HealthStatus = "Not available";
            await _uow.Repository<FlockChicken>().UpdateAsync(entity);
            return true;
        }

        public async Task<bool> RestoreAsync(int id)
        {
            var entity = await _uow.Context.FlockChickens
                .FirstOrDefaultAsync(x => x.FlockId == id && x.HealthStatus != null);
            if (entity == null) return false;

            entity.HealthStatus = "Available";
            await _uow.Repository<FlockChicken>().UpdateAsync(entity);
            return true;
        }
    }
}
