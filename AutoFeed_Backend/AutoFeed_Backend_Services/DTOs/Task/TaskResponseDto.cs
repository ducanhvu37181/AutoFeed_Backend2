namespace AutoFeed_Backend_Services.DTOs.Task;

public class TaskResponseDto
{
    public string Title { get; set; }
    public string Description { get; set; }

    public static TaskResponseDto FromEntity(AutoFeed_Backend_DAO.Models.Task e) => new TaskResponseDto
    {
        Title = e.Title,
        Description = e.Description
    };
}
