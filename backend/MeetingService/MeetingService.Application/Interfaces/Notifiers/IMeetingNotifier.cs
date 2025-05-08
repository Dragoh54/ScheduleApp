using MeetingService.Application.Dtos.MeetingDtos;
using MeetingService.DomainModel.Enums;

namespace MeetingService.Api.Interfaces.Notifiers;

public interface IMeetingNotifier
{
    Task NotifyMeetingAsync(Guid meetingId, string meetingTitle, DateTime date, CancellationToken cancellationToken);
    Task NotifyTimeChangedAsync(Guid meetingId, string meetingTitle, DateTime newStartTime);
    Task NotifyMeetingDeletedAsync(Guid meetingId, string meetingTitle);
    Task NotifyMeetingInformationChangedAsync(Guid meetingId, string oldTitle, string newTitle);
    Task NotifyMeetingStatusChangedAsync(Guid meetingId, string meetingTitle, MeetingStatus newStatus);
}