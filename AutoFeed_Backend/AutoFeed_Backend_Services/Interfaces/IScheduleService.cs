using System.Collections.Generic;
using System.Threading.Tasks;
using ScheduleModel = AutoFeed_Backend_DAO.Models.Schedule;

namespace AutoFeed_Backend_Services.Interfaces;

public interface IScheduleService
{
    Task<List<ScheduleModel>> GetAllAsync();
    Task<ScheduleModel?> GetByIdAsync(int id);
    // returns null if overlap
    Task<ScheduleModel?> CreateAsync(ScheduleModel model);
    // returns false if not found or overlap
    Task<bool> UpdateAsync(ScheduleModel model);
    Task<bool> DeleteAsync(int id);
}
