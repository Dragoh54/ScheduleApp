namespace MeetingService.Application.Dtos.MeetingDto.Requests;

public record RescheduleMeetingRequestDto
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public DateTime? NotifyTime { get; set; }
}