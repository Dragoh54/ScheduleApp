using MeetingService.Application.Interfaces.Services;
using MeetingService.DataAccess.Interfaces.UnitOfWork;
using MeetingService.DomainModel.Exceptions;

namespace MeetingService.Application.Services;

public class ScheduledJobsService(
    IUnitOfWork unitOfWork
    ) : IScheduledJobsService
{
    public async Task ChangeMeetingNotificationScheduleTime(Guid meetingId, DateTime newScheduleTime, CancellationToken cancellationToken)
    {
        var scheduledJobs = await unitOfWork.ScheduledJobRepository.GetScheduledJobsByMeetingId(meetingId,cancellationToken)
            ?? throw new NotFoundException("No scheduled jobs found");
        throw new NotImplementedException();
    }
}