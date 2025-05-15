namespace ScheduleService.Application.Dto.Meetings.Requests.Queries;

public record GetMeetingByIdRequestDto
{
    public Guid MeetingId { get; set; }
}