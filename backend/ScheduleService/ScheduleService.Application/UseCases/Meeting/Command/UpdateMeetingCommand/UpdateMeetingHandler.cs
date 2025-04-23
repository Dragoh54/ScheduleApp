using Mapster;
using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.DataAccess.Interfaces.UnitOfWork;
using ScheduleService.DomainModel.Exceptions;

namespace ScheduleService.Application.UseCases.Meeting.Command.UpdateMeetingCommand;

public class UpdateMeetingHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateMeetingCommand, MeetingDto>
{
    public async Task<MeetingDto> Handle(UpdateMeetingCommand request, CancellationToken cancellationToken)
    {
        var meeting = await unitOfWork.Meetings.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException("Meeting not found");
        
        request.Adapt(meeting);
        
        var updatedMeeting = await unitOfWork.Meetings.UpdateAsync(meeting, cancellationToken)
            ?? throw new BadRequestException("Meeting not updated");
        
        var success = await unitOfWork.Commit(cancellationToken);
        if (!success)
        {
            throw new BadRequestException("Failed to update meeting");
        }
        
        return updatedMeeting.Adapt<MeetingDto>();
    }
}