namespace ScheduleService.Application.Dto.Meetings.Requests.Commands;

public record DeleteMeetingRequestDto
{
    public Guid Id { get; set; }
}