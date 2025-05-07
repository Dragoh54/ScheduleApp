using MeetingService.Api.Extensions;
using MeetingService.Api.Interfaces;
using MeetingService.Api.Interfaces.Hubs;
using MeetingService.Application.Interfaces.Providers;
using Microsoft.AspNetCore.SignalR;

namespace MeetingService.Api.Hubs;

public class ParticipantNotificationHub(
    IUserConnectionManager userConnectionManager,
    IJwtProvider jwtProvider
    ) : Hub<IParticipantNotificationHub>
{
    public async Task<string> SetConnectionId(string userId)
    {
        userConnectionManager.KeepUserConnection(userId, Context.ConnectionId);

        return Context.ConnectionId;
    }
    
    public async override Task OnDisconnectedAsync(Exception exception)
    {
        var connectionId = Context.ConnectionId;
        userConnectionManager.RemoveUserConnection(connectionId);
        
        await base.OnDisconnectedAsync(exception);
    }
}