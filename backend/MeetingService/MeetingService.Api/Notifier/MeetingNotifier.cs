using System.Globalization;
using Hangfire;
using MeetingService.Api.Helpers;
using MeetingService.Api.Hubs;
using MeetingService.Api.Interfaces;
using MeetingService.Api.Interfaces.Hubs;
using MeetingService.Api.Interfaces.Notifiers;
using MeetingService.Application.Dtos.MeetingDtos;
using Microsoft.AspNetCore.SignalR;

namespace MeetingService.Api.Notifier;

public class MeetingNotifier(
    IHubContext<MeetingNotificationHub, IMeetingNotificationHub> hubContext
    ) : IMeetingNotifier
{
    public async Task NotifyMeetingAsync(Guid meetingId, string meetingTitle, DateTime date)
    {
        var stringId = meetingId.ToString();
        var stringDate = date.ToString(CultureInfo.InvariantCulture);
        
        var notifyTime = date.AddDays(-1);
        
        if (notifyTime <= DateTime.UtcNow)
        {
            await hubContext.Clients.Group(meetingId.ToString()).MeetingNotification(stringId, stringDate, meetingTitle);
        }
        else
        {
            BackgroundJob.Schedule<MeetingHubHelper>(h =>
                    h.SendData(stringId, meetingTitle, stringDate),
                notifyTime);
        }
    }

    public async Task NotifyTimeChangedAsync(Guid meetingId, string meetingTitle, DateTime newStartTime)
    {
        await hubContext
            .Clients
            .Group(meetingId.ToString())
            .MeetingTimeChanged(meetingId.ToString(), meetingTitle, newStartTime.ToString(CultureInfo.InvariantCulture));
    }

    public async Task NotifyMeetingDeletedAsync(Guid meetingId, string meetingTitle)
    {
        await hubContext
            .Clients
            .Group(meetingId.ToString())
            .MeetingDeleted(meetingId.ToString(), meetingTitle);
    }
}