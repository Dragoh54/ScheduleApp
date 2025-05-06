namespace MeetingService.Api.Interfaces.Notifiers;

public interface IMeetingNotifier
{
    Task NotifyTimeChangedAsync(Guid meetingId, DateTime newStartTime);
    Task NotifyMeetingDeletedAsync(Guid meetingId);
}