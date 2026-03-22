using AutoFeed_Backend_DAO.Models;
using AutoFeed_Backend_Repositories.UnitOfWork;
using AutoFeed_Backend_Services.DTOs.Barn;
using AutoFeed_Backend_Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AutoFeed_Backend_Services.Services
{
    public class BarnService : IBarnService
    {
        private readonly IUnitOfWork _uow;

        public BarnService(IUnitOfWork uow) => _uow = uow;

        private static BarnResponseDto ToDto(Barn e) => new()
        {
            BarnId = e.BarnId,
            Temperature = e.Temperature,
            Humidity = e.Humidity,
            Type = e.Type,
            Area = e.Area,
            Status = e.Status,
            CreateDate = e.CreateDate
        };

        public async Task<List<BarnResponseDto>> GetAllAsync(bool status = false)
        {
            var query = _uow.Context.Barns.AsQueryable();
            if (!status) query = query.Where(x => x.Status);
            return await query.Select(e => new BarnResponseDto
            {
                BarnId = e.BarnId,
                Temperature = e.Temperature,
                Humidity = e.Humidity,
                Type = e.Type,
                Area = e.Area,
                Status = e.Status,
                CreateDate = e.CreateDate
            }).ToListAsync();
        }

        public async Task<BarnResponseDto?> GetByIdAsync(int id)
        {
            var entity = await _uow.Context.Barns
                .FirstOrDefaultAsync(x => x.BarnId == id);
            return entity == null ? null : ToDto(entity);
        }

        public async Task<List<BarnResponseDto>> SearchAsync(BarnSearchDto filter)
        {
            var query = _uow.Context.Barns.AsQueryable();

            if (!filter.Status)
                query = query.Where(x => x.Status);

            if (!string.IsNullOrWhiteSpace(filter.Type))
                query = query.Where(x => x.Type.Contains(filter.Type));

            if (filter.Area.HasValue)
                query = query.Where(x => x.Area == filter.Area);

            return await query.Select(e => new BarnResponseDto
            {
                BarnId = e.BarnId,
                Temperature = e.Temperature,
                Humidity = e.Humidity,
                Type = e.Type,
                Area = e.Area,
                Status = e.Status,
                CreateDate = e.CreateDate
            }).ToListAsync();
        }

        public async Task<BarnResponseDto?> CreateAsync(CreateBarnRequestDto dto)
        {
            var entity = new Barn
            {
                Type = dto.Type,
                Area = dto.Area,
                Status = dto.Status,
                CreateDate = dto.CreateDate
            };
            await _uow.Repository<Barn>().CreateAsync(entity);
            return ToDto(entity);
        }

        public async Task<bool> UpdateAsync(int id, UpdateBarnDto dto)
        {
            var entity = await _uow.Context.Barns
                .FirstOrDefaultAsync(x => x.BarnId == id && x.Status);
            if (entity == null) return false;

            entity.Area = dto.Area;
            entity.Type = dto.Type;

            await _uow.Repository<Barn>().UpdateAsync(entity);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _uow.Context.Barns
                .FirstOrDefaultAsync(x => x.BarnId == id && x.Status);
            if (entity == null) return false;

            entity.Status = false;
            await _uow.Repository<Barn>().UpdateAsync(entity);
            return true;
        }

        public async Task<bool> RestoreAsync(int id)
        {
            var entity = await _uow.Context.Barns
                .FirstOrDefaultAsync(x => x.BarnId == id && !x.Status);
            if (entity == null) return false;

            entity.Status = true;
            await _uow.Repository<Barn>().UpdateAsync(entity);
            return true;
        }
    }
}
