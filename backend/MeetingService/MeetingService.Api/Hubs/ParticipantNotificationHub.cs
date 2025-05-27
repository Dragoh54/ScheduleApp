using MeetingService.Api.Extensions;
using MeetingService.Api.Interfaces;
using MeetingService.Api.Interfaces.Hubs;
using MeetingService.Application.Interfaces.Providers;
using Microsoft.AspNetCore.SignalR;

namespace MeetingService.Api.Hubs;

public class ParticipantNotificationHub : Hub<IParticipantNotificationHub>
{
    public ParticipantNotificationHub(IUserConnectionManager userConnectionManager)
    {
        _userConnectionManager = userConnectionManager;
    }
    
    private readonly IUserConnectionManager _userConnectionManager;
    
    public async Task<string> SetConnectionId(string userId)
    {
        _userConnectionManager.KeepUserConnection(userId, Context.ConnectionId);

        return Context.ConnectionId;
    }
    
    public override async Task OnDisconnectedAsync(Exception exception)
    {
        var connectionId = Context.ConnectionId;
        _userConnectionManager.RemoveUserConnection(connectionId);
        
        await base.OnDisconnectedAsync(exception);
    }
}