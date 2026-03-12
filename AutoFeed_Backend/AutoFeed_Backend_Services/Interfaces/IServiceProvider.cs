namespace AutoFeed_Backend_Services.Interfaces;

public interface IServiceProvider
{
    ITaskService TaskService { get; }
    IScheduleService ScheduleService { get; }
    ILargeChickenService LargeChickenService { get; }
    IUserService UserService { get; }
}
