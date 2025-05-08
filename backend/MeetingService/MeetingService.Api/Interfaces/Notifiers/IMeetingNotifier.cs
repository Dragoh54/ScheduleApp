using MeetingService.Application.Dtos.MeetingDtos;

namespace MeetingService.Api.Interfaces.Notifiers;

public interface IMeetingNotifier
{
    Task NotifyMeetingAsync(Guid meetingId, string meetingTitle, DateTime date);
    Task NotifyTimeChangedAsync(Guid meetingId, string meetingTitle, DateTime newStartTime);
    Task NotifyMeetingDeletedAsync(Guid meetingId, string meetingTitle);
}