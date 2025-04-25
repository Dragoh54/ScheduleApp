using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.DataAccess.Interfaces.UnitOfWork;

namespace MeetingService.Application.UseCases.Meetings.Command.RescheduleMeetingCommand;

public class RescheduleMeetingHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<RescheduleMeetingCommand, MeetingDto>
{
    public Task<MeetingDto> Handle(RescheduleMeetingCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}