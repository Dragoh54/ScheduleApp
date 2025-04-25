using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.DataAccess.Interfaces.UnitOfWork;

namespace MeetingService.Application.UseCases.Meetings.Command.CreateMeetingCommand;

public class CreateMeetingHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<CreateMeetingCommand, MeetingDto>
{
    public Task<MeetingDto> Handle(CreateMeetingCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}