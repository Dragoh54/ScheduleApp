namespace ScheduleService.Application.Dto.Meetings.Requests.Queries;

public record GetUpcomingMeetingsRequestDto
{
    public Guid UserId { get; set; }
}