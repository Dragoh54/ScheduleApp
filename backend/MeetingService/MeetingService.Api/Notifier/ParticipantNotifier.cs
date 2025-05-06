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
    public async Task NotifyInvitedAsync(Guid meetingId, Guid userId)
    {
        var connections = userConnectionManager.GetUserConnections(userId.ToString());
        foreach (var connectionId in connections)
        {
            await hubContext.Clients.Client(connectionId)
                .InvitedToMeeting(meetingId.ToString());
        }
    }

    public async Task NotifyJoinedAsync(Guid meetingId, Guid userId)
    {
        var connections = userConnectionManager.GetUserConnections(userId.ToString());
        foreach (var connectionId in connections)
        {
            await hubContext.Clients.Client(connectionId)
                .JoinedToMeeting(meetingId.ToString());
        }
    }

    public async Task NotifyRemovedAsync(Guid meetingId, Guid userId)
    {
        var connections = userConnectionManager.GetUserConnections(userId.ToString());
        foreach (var connectionId in connections)
        {
            await hubContext.Clients.Client(connectionId)
                .RemovedFromMeeting(meetingId.ToString());
        }
    }
}