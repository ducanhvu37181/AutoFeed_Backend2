namespace AutoFeed_Backend_Services.DTOs.Task;

public class TaskDto
{
    public int TaskId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool Status { get; set; }

    public static TaskDto FromEntity(AutoFeed_Backend_DAO.Models.Task e) => new TaskDto
    {
        TaskId = e.TaskId,
        Title = e.Title,
        Description = e.Description,
        Status = e.Status
    };
}
