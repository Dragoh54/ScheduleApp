using MeetingService.Api.Hubs;
using MeetingService.Api.Interfaces;
using MeetingService.Api.Interfaces.Hubs;
using MeetingService.Api.Interfaces.Notifiers;
using Microsoft.AspNetCore.SignalR;

namespace MeetingService.Api.Notifier;

public class ParticipantNotifier(
    IUserConnectionManager userConnectionManager,
    IHubContext<ParticipantNotificationHub, IParticipantNotificationHub> hubContext
    ) : IParticipantNotifier
{
    public async Task NotifyInvitedAsync(Guid meetingId, Guid userId, string meetingTitle)
    {
        await NotifyAsync(userId, client =>
            client.InvitedToMeeting(meetingId.ToString(), meetingTitle));
    }

    public async Task NotifyJoinedAsync(Guid meetingId, Guid userId, string meetingTitle)
    {
        await NotifyAsync(userId, client =>
            client.JoinedToMeeting(meetingId.ToString(), meetingTitle));
    }

    public async Task NotifyRemovedAsync(Guid meetingId, Guid userId, string meetingTitle)
    {
        await NotifyAsync(userId, client =>
            client.RemovedFromMeeting(meetingId.ToString(), meetingTitle));
    }
    private async Task NotifyAsync(Guid userId, Func<IParticipantNotificationHub, Task> notifyAction)
    {
        var connections = userConnectionManager.GetUserConnections(userId.ToString());

        foreach (var connectionId in connections)
        {
            var client = hubContext.Clients.Client(connectionId);
            await notifyAction(client);
        } 
    }
}