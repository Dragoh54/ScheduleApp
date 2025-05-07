namespace MeetingService.Application.Dtos.MeetingDtos;

public record CreateMeetingDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}