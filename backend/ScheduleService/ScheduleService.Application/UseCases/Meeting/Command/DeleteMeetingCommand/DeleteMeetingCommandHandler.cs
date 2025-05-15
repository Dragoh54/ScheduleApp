using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.Application.Interfaces.UnitOfWork;
using ScheduleService.DomainModel.Enums;
using ScheduleService.DomainModel.Exceptions;

namespace ScheduleService.Application.UseCases.Meeting.Command.DeleteMeetingCommand;

public class DeleteMeetingCommandHandler : IRequestHandler<DeleteMeetingCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteMeetingCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<bool> Handle(DeleteMeetingCommand request, CancellationToken cancellationToken)
    {
        var meeting = await _unitOfWork.Meetings.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException("Meeting not found");
        
        await _unitOfWork.Meetings.DeleteAsync(meeting.Id, cancellationToken);

        var success = await _unitOfWork.Commit(cancellationToken);
        if (!success)
        {
            throw new BadRequestException("Failed to delete meeting");
        }

        return success;
    }
}