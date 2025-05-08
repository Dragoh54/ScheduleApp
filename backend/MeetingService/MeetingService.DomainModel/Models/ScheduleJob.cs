namespace MeetingService.DomainModel.Models;

public class ScheduledJob : IdEntity
{
    public Guid MeetingId { get; set; }
    public string JobId { get; set; } = string.Empty;
    public DateTime ExecuteAt { get; set; }
}