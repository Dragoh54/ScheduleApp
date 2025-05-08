namespace MeetingService.Api.Interfaces.Helpers;

public interface IMeetingHubHelper
{
    Task SendMeetingNotification(string meetingId, string meetingTitle, string date);
}