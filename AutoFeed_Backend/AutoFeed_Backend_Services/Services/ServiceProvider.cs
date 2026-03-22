using AutoFeed_Backend_Services.Interfaces;

namespace AutoFeed_Backend_Services.Services;

public class ServiceProvider : AutoFeed_Backend_Services.Interfaces.IServiceProvider
{
    public IAuthService AuthService { get; }
    public IBarnService BarnService { get; }
    public IFlockChickenService FlockChickenService { get; }
    public ITaskService TaskService { get; }
    public IScheduleService ScheduleService { get; }
    public ILargeChickenService LargeChickenService { get; }
    public IUserService UserService { get; }

    public ServiceProvider(IAuthService authService, IBarnService barnService, IFlockChickenService flockChickenService, ITaskService taskService, IScheduleService scheduleService, ILargeChickenService largeChickenService, IUserService userService)
    {
        AuthService = authService;
        BarnService = barnService;
        FlockChickenService = flockChickenService;
        TaskService = taskService;
        ScheduleService = scheduleService;
        LargeChickenService = largeChickenService;
        UserService = userService;
    }
}
