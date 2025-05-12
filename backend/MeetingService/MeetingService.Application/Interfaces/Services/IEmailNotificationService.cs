using MeetingService.Application.Dtos.NotificationDtos;

namespace MeetingService.Application.Interfaces.Services;

public interface IEmailNotificationService
{
    public Task SendNotificationAtNotifyTime(Guid meetingId, EmailNotificationDto dto, DateTime notifyTime, CancellationToken cancellationToken);

    public Task SendNotification(EmailNotificationDto dto, CancellationToken cancellationToken);
}