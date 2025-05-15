namespace ScheduleService.Application.Dto.Meetings.Requests.Queries;

public record GetUserMeetingsRequestDto
{
    public Guid UserId { get; set; }
}