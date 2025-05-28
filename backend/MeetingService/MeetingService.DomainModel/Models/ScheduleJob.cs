namespace MeetingService.DomainModel.Models;

public class ScheduledJob : IdEntity
{
    public ScheduledJob()
    {
    }

    public ScheduledJob(Guid meetingId, string jobId, DateTime executeAt)
    {
        Id = Guid.NewGuid();
        MeetingId = meetingId;
        JobId = jobId;
        ExecuteAt = executeAt;
    }
    public Guid MeetingId { get; set; }
    public string JobId { get; set; } = string.Empty;
    public DateTime ExecuteAt { get; set; }
}