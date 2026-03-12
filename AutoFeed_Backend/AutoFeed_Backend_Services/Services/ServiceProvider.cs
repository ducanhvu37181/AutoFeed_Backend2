using AutoFeed_Backend_Services.Interfaces;

namespace AutoFeed_Backend_Services.Services;

public class ServiceProvider : AutoFeed_Backend_Services.Interfaces.IServiceProvider
{
    public ITaskService TaskService { get; }
    public IScheduleService ScheduleService { get; }
    public ILargeChickenService LargeChickenService { get; }
    public IUserService UserService { get; }

    public ServiceProvider(ITaskService taskService, IScheduleService scheduleService, ILargeChickenService largeChickenService, IUserService userService)
    {
        TaskService = taskService;
        ScheduleService = scheduleService;
        LargeChickenService = largeChickenService;
        UserService = userService;
    }
}
