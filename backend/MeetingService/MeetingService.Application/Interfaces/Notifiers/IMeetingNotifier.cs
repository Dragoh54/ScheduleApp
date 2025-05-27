using MeetingService.DomainModel.Enums;

namespace MeetingService.Api.Interfaces.Notifiers;

public interface IMeetingNotifier
{
    Task NotifyOnTimeAsync(Guid meetingId, string meetingTitle, DateTime newStartTime, DateTime notifyTime, CancellationToken cancellationToken);
    Task NotifyMeetingDeletedAsync(Guid meetingId, string meetingTitle);
    Task NotifyMeetingInformationChangedAsync(Guid meetingId, string oldTitle, string newTitle);
    Task NotifyMeetingStatusChangedAsync(Guid meetingId, string meetingTitle, MeetingStatus newStatus);
}