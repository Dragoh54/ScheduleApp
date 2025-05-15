using ScheduleService.DomainModel.Enums;

namespace ScheduleService.Application.Dto.Meetings.Requests.Commands;

public class UpdateMeetingRequestDto
{
    public DateTime StartTime { get; init; }
    public DateTime EndTime { get; init; }
    public MeetingStatus Status { get; init; }
}