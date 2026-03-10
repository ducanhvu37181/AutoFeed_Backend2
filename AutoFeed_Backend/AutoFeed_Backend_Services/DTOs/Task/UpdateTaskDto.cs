namespace AutoFeed_Backend_Services.DTOs.Task;

public class UpdateTaskDto
{
    public int TaskId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool Status { get; set; }

    public AutoFeed_Backend_DAO.Models.Task ToEntity() => new AutoFeed_Backend_DAO.Models.Task
    {
        TaskId = TaskId,
        Title = Title,
        Description = Description,
        Status = Status
    };
}
