using ScheduleService.DomainModel.Enums;

namespace ScheduleService.Application.Dto.Meetings.Requests.Commands;

public record UpdateMeetingStatusRequestDto
{
    public MeetingStatus Status { get; set; }
}