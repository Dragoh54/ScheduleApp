using ScheduleService.DomainModel.Enums;

namespace ScheduleService.Application.Dto.Meetings.Requests.Commands;

public class UpdateMeetingRequestDto
{
    public Guid UserId { get; init; }
    public DateTime StartTime { get; init; }
    public DateTime EndTime { get; init; }
    public MeetingStatus Status { get; init; }
}