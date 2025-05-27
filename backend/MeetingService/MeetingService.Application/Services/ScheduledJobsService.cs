using Hangfire;
using MeetingService.Application.Interfaces.Services;
using MeetingService.Application.Interfaces.UnitOfWork;
using MeetingService.DomainModel.Exceptions;

namespace MeetingService.Application.Services;

public class ScheduledJobsService : IScheduledJobsService
{
    public ScheduledJobsService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    private readonly IUnitOfWork _unitOfWork;
    
    public async Task DeleteScheduledJobs(Guid meetingId, CancellationToken cancellationToken)
    {
        var currentScheduledJobs = await _unitOfWork.ScheduledJobRepository.GetScheduledJobsByMeetingId(meetingId, cancellationToken)
                                   ?? throw new NotFoundException("Scheduled jobs not found");

        foreach (var job in currentScheduledJobs)
        {
            var success = BackgroundJob.Delete(job.JobId);
            if (!success)
            {
                throw new BadRequestException("Scheduled job could not be deleted");
            }
            
            await _unitOfWork.ScheduledJobRepository.Delete(job, cancellationToken);
        }

        await _unitOfWork.SaveChangesAsync();
    }
}