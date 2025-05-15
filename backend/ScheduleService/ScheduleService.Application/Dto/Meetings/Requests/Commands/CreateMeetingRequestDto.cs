using ScheduleService.DomainModel.Enums;

namespace ScheduleService.Application.Dto.Meetings.Requests.Commands;

public record CreateMeetingRequestDto
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public MeetingStatus Status { get; set; }
}