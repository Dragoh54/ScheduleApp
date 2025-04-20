using Mapster;
using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.DataAccess.Interfaces.UnitOfWork;
using ScheduleService.DomainModel.Exceptions;

namespace ScheduleService.Application.UseCases.Meeting.Command.UpdateMeetingStatusCommand;

public class UpdateMeetingStatusHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateMeetingStatusCommand, MeetingDto>
{
    public async Task<MeetingDto> Handle(UpdateMeetingStatusCommand request, CancellationToken cancellationToken)
    {
        var meeting = await unitOfWork.Meetings.UpdateMeetingStatusAsync(request.Id, request.Status, cancellationToken)
            ?? throw new NotFoundException("Meeting not found");
        
        var success = await unitOfWork.Commit(cancellationToken);
        if (!success)
        {
            throw new BadRequestException("Failed to create meeting");
        }
        
        return meeting.Adapt<MeetingDto>();
    }
}