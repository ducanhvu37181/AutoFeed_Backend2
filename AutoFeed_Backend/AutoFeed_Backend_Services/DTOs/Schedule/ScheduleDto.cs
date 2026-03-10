using System;

namespace AutoFeed_Backend_Services.DTOs.Schedule;

public class ScheduleDto
{
    public int SchedId { get; set; }
    public int? UserId { get; set; }
    public int? TaskId { get; set; }
    public int? CbarnId { get; set; }
    public string Description { get; set; }
    public bool Status { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime? CreatedDate { get; set; }

    public static ScheduleDto FromEntity(AutoFeed_Backend_DAO.Models.Schedule e) => new ScheduleDto
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
}
