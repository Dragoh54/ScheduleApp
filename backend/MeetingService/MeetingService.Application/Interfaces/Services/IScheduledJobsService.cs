namespace MeetingService.Application.Interfaces.Services;

public interface IScheduledJobsService
{
    Task ChangeMeetingNotificationScheduleTime(Guid meetingId, DateTime newScheduleTime, CancellationToken cancellationToken);
}