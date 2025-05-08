namespace MeetingService.Api.Interfaces.Hubs;

public interface IParticipantNotificationHub
{
    public Task InvitedToMeeting(string meetingId, string meetingTitle);
    public Task JoinedToMeeting(string meetingId, string meetingTitle);
    public Task RemovedFromMeeting(string meetingId, string meetingTitle);
}