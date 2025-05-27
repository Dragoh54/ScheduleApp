namespace MeetingService.Application.Interfaces.Services;

public interface IScheduledJobsService
{
    Task DeleteScheduledJobs(Guid meetingId, CancellationToken cancellationToken);
}