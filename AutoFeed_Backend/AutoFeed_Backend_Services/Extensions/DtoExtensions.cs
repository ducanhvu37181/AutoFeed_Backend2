using AutoFeed_Backend_DAO.Models;
using AutoFeed_Backend_Services.DTOs.Task;
using AutoFeed_Backend_Services.DTOs.Schedule;

namespace AutoFeed_Backend_Services.Extensions;

public static class DtoExtensions
{
    public static TaskDto ToDto(this AutoFeed_Backend_DAO.Models.Task e) => new TaskDto
    {
        TaskId = e.TaskId,
        Title = e.Title,
        Description = e.Description,
        Status = e.Status
    };

    public static AutoFeed_Backend_DAO.Models.Task ToEntity(this TaskDto d) => new AutoFeed_Backend_DAO.Models.Task
    {
        TaskId = d.TaskId,
        Title = d.Title,
        Description = d.Description,
        Status = d.Status
    };

    public static AutoFeed_Backend_DAO.Models.Task ToEntity(this CreateTaskDto d) => new AutoFeed_Backend_DAO.Models.Task
    {
        TaskId = 0,
        Title = d.Title,
        Description = d.Description,
        Status = d.Status
    };

    public static AutoFeed_Backend_DAO.Models.Task ToEntity(this UpdateTaskDto d) => new AutoFeed_Backend_DAO.Models.Task
    {
        TaskId = d.TaskId,
        Title = d.Title,
        Description = d.Description,
        Status = d.Status
    };

    public static ScheduleDto ToDto(this AutoFeed_Backend_DAO.Models.Schedule e) => new ScheduleDto
    {
        SchedId = e.SchedId,
        UserId = e.UserId,
        TaskId = e.TaskId,
        CbarnId = e.CbarnId,
        Description = e.Description,
        Status = e.Status,
        StartDate = e.StartDate,
        EndDate = e.EndDate,
        CreatedDate = e.CreatedDate
    };

    public static AutoFeed_Backend_DAO.Models.Schedule ToEntity(this ScheduleDto d) => new AutoFeed_Backend_DAO.Models.Schedule
    {
        SchedId = d.SchedId,
        UserId = d.UserId,
        TaskId = d.TaskId,
        CbarnId = d.CbarnId,
        Description = d.Description,
        Status = d.Status,
        StartDate = d.StartDate,
        EndDate = d.EndDate,
        CreatedDate = d.CreatedDate
    };

    public static AutoFeed_Backend_DAO.Models.Schedule ToEntity(this CreateScheduleDto d) => new AutoFeed_Backend_DAO.Models.Schedule
    {
        SchedId = 0,
        UserId = d.UserId,
        TaskId = d.TaskId,
        CbarnId = d.CbarnId,
        Description = d.Description,
        Status = d.Status,
        StartDate = d.StartDate,
        EndDate = d.EndDate,
        CreatedDate = null
    };

    public static AutoFeed_Backend_DAO.Models.Schedule ToEntity(this UpdateScheduleDto d) => new AutoFeed_Backend_DAO.Models.Schedule
    {
        SchedId = d.SchedId,
        UserId = d.UserId,
        TaskId = d.TaskId,
        CbarnId = d.CbarnId,
        Description = d.Description,
        Status = d.Status,
        StartDate = d.StartDate,
        EndDate = d.EndDate,
        CreatedDate = d.SchedId == 0 ? null : d.StartDate // placeholder; service will set CreatedDate
    };
}
