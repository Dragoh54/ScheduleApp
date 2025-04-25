using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.DataAccess.Interfaces.UnitOfWork;

namespace MeetingService.Application.UseCases.Meetings.Command.DeleteMeetingCommand;

public class DeleteMeetingHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<DeleteMeetingCommand, MeetingDto>
{
    public Task<MeetingDto> Handle(DeleteMeetingCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}