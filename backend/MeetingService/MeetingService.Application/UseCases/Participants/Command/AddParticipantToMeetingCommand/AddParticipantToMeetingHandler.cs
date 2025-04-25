using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.DataAccess.Interfaces.UnitOfWork;

namespace MeetingService.Application.UseCases.Participants.Command.AddParticipantToMeetingCommand;

public class AddParticipantToMeetingHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<AddParticipantToMeetingCommand, ParticipantDto>
{
    public Task<ParticipantDto> Handle(AddParticipantToMeetingCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}