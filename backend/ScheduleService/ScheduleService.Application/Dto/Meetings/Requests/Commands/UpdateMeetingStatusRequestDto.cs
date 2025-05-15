using ScheduleService.DomainModel.Enums;

namespace ScheduleService.Application.Dto.Meetings.Requests.Commands;

public record UpdateMeetingStatusRequestDto
{
    public Guid Id { get; set; }
    public MeetingStatus Status { get; set; }
}