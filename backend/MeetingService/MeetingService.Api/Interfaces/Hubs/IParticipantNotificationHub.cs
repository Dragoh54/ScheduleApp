namespace MeetingService.Api.Interfaces.Hubs;

public interface IParticipantNotificationHub
{
    Task InvitedToMeeting(string meetingId);
    Task JoinedToMeeting(string meetingId);
    Task RemovedFromMeeting(string meetingId);
}