using AutoFeed_Backend_Services.Interfaces;

namespace AutoFeed_Backend_Services.Services;

public class ServiceProvider : AutoFeed_Backend_Services.Interfaces.IServiceProvider
{
    public ITaskService TaskService { get; }
    public IScheduleService ScheduleService { get; }

    public ServiceProvider(ITaskService taskService, IScheduleService scheduleService)
    {
        TaskService = taskService;
        ScheduleService = scheduleService;
    }
}
