using MeetingService.Application.Dtos.MeetingDtos;

namespace MeetingService.Api.Interfaces.Notifiers;

public interface IMeetingNotifier
{
    Task NotifyMeetingAsync(Guid meetingId, DateTime date);
    Task NotifyTimeChangedAsync(Guid meetingId, DateTime newStartTime);
    Task NotifyMeetingDeletedAsync(Guid meetingId);
}