namespace ScheduleService.Application.Dto.Meetings.Requests.Queries;

public record GetMeetingsOnDateRequestDto
{
    public DateTime Date { get; set; }
}