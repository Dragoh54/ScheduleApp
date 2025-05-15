namespace ScheduleService.Application.Dto.Meetings.Requests.Queries;

public record GetMeetingByIdRequestDto
{
    public Guid Id { get; set; }
}