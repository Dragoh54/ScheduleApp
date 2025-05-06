namespace MeetingService.Api.Interfaces.Hubs;

public interface IMeetingNotificationHub
{
    Task MeetingNotification(string meetingId, string date);
    Task MeetingTimeChanged(string meetingId, string newStartTime);
    Task MeetingDeleted(string meetingId);
}