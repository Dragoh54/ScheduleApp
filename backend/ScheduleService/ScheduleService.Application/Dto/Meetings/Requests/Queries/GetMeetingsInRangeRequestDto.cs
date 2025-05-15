namespace ScheduleService.Application.Dto.Meetings.Requests.Queries;

public record GetMeetingsInRangeRequestDto
{
    public Guid UserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}