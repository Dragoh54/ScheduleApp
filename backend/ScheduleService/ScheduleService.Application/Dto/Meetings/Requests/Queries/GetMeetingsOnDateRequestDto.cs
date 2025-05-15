namespace ScheduleService.Application.Dto.Meetings.Requests.Queries;

public record GetMeetingsOnDateRequestDto
{
    public Guid UserId { get; set; }
    public DateTime Date { get; set; }
}