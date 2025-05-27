using System.Globalization;
using Hangfire;
using MeetingService.Api.Helpers;
using MeetingService.Api.Hubs;
using MeetingService.Api.Interfaces.Hubs;
using MeetingService.Api.Interfaces.Notifiers;
using MeetingService.Application.Interfaces.UnitOfWork;
using MeetingService.DomainModel.Enums;
using MeetingService.DomainModel.Models;
using Microsoft.AspNetCore.SignalR;

namespace MeetingService.Api.Notifiers;

public class MeetingNotifier : IMeetingNotifier
{
    public MeetingNotifier(
        IHubContext<MeetingNotificationHub, IMeetingNotificationHub> hubContext,
        IUnitOfWork unitOfWork
    )
    {
        _hubContext = hubContext;
        _unitOfWork = unitOfWork;
    }

    private readonly IHubContext<MeetingNotificationHub, IMeetingNotificationHub> _hubContext;
    private readonly IUnitOfWork _unitOfWork;
    
    public async Task NotifyOnTimeAsync(Guid meetingId, string meetingTitle, DateTime newStartTime, 
        DateTime notifyTime, CancellationToken cancellationToken)
    {
        var stringDate = newStartTime.ToString(CultureInfo.InvariantCulture);
        
        if (notifyTime <= DateTime.UtcNow)
        {
            await _hubContext.Clients.Group(meetingId.ToString()).MeetingNotification(meetingId.ToString(), stringDate, meetingTitle);
        }
        else
        {
            await SetScheduledJob(meetingId, meetingTitle, stringDate, notifyTime, cancellationToken);
        }
    }

    public async Task NotifyMeetingDeletedAsync(Guid meetingId, string meetingTitle)
    {
        await _hubContext
            .Clients
            .Group(meetingId.ToString())
            .MeetingDeleted(meetingId.ToString(), meetingTitle);
    }

    public async Task NotifyMeetingInformationChangedAsync(Guid meetingId, string oldTitle, string newTitle)
    {
        await _hubContext
            .Clients
            .Group(meetingId.ToString())
            .MeetingInformationUpdated(meetingId.ToString(), oldTitle, newTitle);
    }

    public async Task NotifyMeetingStatusChangedAsync(Guid meetingId, string meetingTitle, MeetingStatus newStatus)
    {
        await _hubContext
            .Clients
            .Group(meetingId.ToString())
            .MeetingStatusChanged(meetingId.ToString(), meetingTitle, newStatus.ToString());
    }

    private async Task SetScheduledJob(Guid meetingId, string meetingTitle, string date, 
        DateTime notifyTime, CancellationToken cancellationToken)
    {
        var jobId = BackgroundJob.Schedule<MeetingHubHelper>(h =>
                h.SendMeetingNotification(meetingId.ToString(), meetingTitle, date), 
            notifyTime);

        var scheduledJob = new ScheduledJob(meetingId, jobId, notifyTime);

        await _unitOfWork.ScheduledJobRepository.Add(scheduledJob, cancellationToken);

        await _unitOfWork.SaveChangesAsync();
    } 
}