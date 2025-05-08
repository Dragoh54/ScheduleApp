namespace MeetingService.Api.Interfaces.Helpers;

public interface IMeetingHubHelper
{
    Task SendData(string meetingId, string meetingTitle, string date);
}