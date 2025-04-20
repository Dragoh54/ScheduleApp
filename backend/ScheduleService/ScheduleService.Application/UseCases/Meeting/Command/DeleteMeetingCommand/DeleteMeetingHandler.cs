using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.DataAccess.Interfaces.UnitOfWork;
using ScheduleService.DomainModel.Exceptions;

namespace ScheduleService.Application.UseCases.Meeting.Command.DeleteMeetingCommand;

public class DeleteMeetingHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<DeleteMeetingCommand, bool>
{
    public async Task<bool> Handle(DeleteMeetingCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.Meetings.DeleteAsync(request.Id, cancellationToken);

        var success = await unitOfWork.Commit(cancellationToken);
        if (!success)
        {
            throw new BadRequestException("Failed to delete meeting");
        }

        return success;
    }
}