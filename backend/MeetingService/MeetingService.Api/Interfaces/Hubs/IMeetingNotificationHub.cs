namespace MeetingService.Api.Interfaces.Hubs;

public interface IMeetingNotificationHub
{
    Task MeetingTimeChanged(string meetingId, string newStartTime);
    Task MeetingDeleted(string meetingId);
}