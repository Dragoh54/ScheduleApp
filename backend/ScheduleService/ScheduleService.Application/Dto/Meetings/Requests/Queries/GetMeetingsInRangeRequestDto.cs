namespace ScheduleService.Application.Dto.Meetings.Requests.Queries;

public record GetMeetingsInRangeRequestDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}