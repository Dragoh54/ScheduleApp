namespace MeetingService.Api.Interfaces.Hubs;

public interface IMeetingNotificationHub
{
    Task MeetingNotification(string meetingId, string meetingTitle, string date);
    Task MeetingTimeChanged(string meetingId, string meetingTitle, string newStartTime);
    Task MeetingDeleted(string meetingId, string meetingTitle);
}