namespace MeetingService.Application.Dtos.MeetingDtos;

public record RescheduleMeetingDto
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}