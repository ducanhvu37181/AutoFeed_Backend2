using System.Collections.Generic;
using System.Threading.Tasks;
using TaskModel = AutoFeed_Backend_DAO.Models.Task;
using AutoFeed_Backend_Repositories.BasicRepo;
using AutoFeed_Backend_Repositories.UnitOfWork;
using AutoFeed_Backend_Services.Interfaces;

namespace AutoFeed_Backend_Services.Services;

public class TaskService : ITaskService
{
    private readonly IUnitOfWork _uow;

    public TaskService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<TaskModel> CreateAsync(TaskModel model)
    {
        var repo = _uow.Repository<TaskModel>();
        model.TaskId = 0;
        await repo.CreateAsync(model);
        return model;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var repo = _uow.Repository<TaskModel>();
        var existing = await repo.GetByIdAsync(id);
        if (existing == null) return false;
        // Soft-delete: set Status to false and update
        existing.Status = false;
        await repo.UpdateAsync(existing);
        return true;
    }

    public async Task<List<TaskModel>> GetAllAsync()
    {
        var repo = _uow.Repository<TaskModel>();
        return await repo.GetAllAsync();
    }

    public async Task<TaskModel?> GetByIdAsync(int id)
    {
        var repo = _uow.Repository<TaskModel>();
        return await repo.GetByIdAsync(id);
    }

    public async Task<bool> UpdateAsync(TaskModel model)
    {
        var repo = _uow.Repository<TaskModel>();
        var existing = await repo.GetByIdAsync(model.TaskId);
        if (existing == null) return false;
        await repo.UpdateAsync(model);
        return true;
    }
}
