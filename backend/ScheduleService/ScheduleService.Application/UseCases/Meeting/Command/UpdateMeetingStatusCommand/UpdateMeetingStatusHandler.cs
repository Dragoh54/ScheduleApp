using Mapster;
using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.DataAccess.Interfaces.UnitOfWork;
using ScheduleService.DomainModel.Enums;
using ScheduleService.DomainModel.Exceptions;

namespace ScheduleService.Application.UseCases.Meeting.Command.UpdateMeetingStatusCommand;

public class UpdateMeetingStatusHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateMeetingStatusCommand, MeetingDto>
{
    public async Task<MeetingDto> Handle(UpdateMeetingStatusCommand request, CancellationToken cancellationToken)
    {
        var meeting = await unitOfWork.Meetings.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException("Meeting not found");

        var isStatusInvalid = meeting.Status == MeetingStatus.Cancelled;
        if (isStatusInvalid)
        {
            throw new BadRequestException("This meeting is cancelled");
        }
        
        await unitOfWork.Meetings.UpdateMeetingStatusAsync(request.Id, request.Status, cancellationToken);
        
        var success = await unitOfWork.Commit(cancellationToken);
        if (!success)
        {
            throw new BadRequestException("Failed to create meeting");
        }
        
        var updatedMeeting = await unitOfWork.Meetings.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException("Meeting not found");
        
        return updatedMeeting.Adapt<MeetingDto>();
    }
}