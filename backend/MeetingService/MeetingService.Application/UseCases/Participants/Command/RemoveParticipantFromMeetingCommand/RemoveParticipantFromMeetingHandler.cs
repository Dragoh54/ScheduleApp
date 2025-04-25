using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.DataAccess.Interfaces.UnitOfWork;

namespace MeetingService.Application.UseCases.Participants.Command.RemoveParticipantFromMeetingCommand;

public class RemoveParticipantFromMeetingHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<RemoveParticipantFromMeetingCommand, ParticipantDto>
{
    public Task<ParticipantDto> Handle(RemoveParticipantFromMeetingCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}