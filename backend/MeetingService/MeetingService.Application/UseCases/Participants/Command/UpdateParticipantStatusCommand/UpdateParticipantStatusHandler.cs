using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.DataAccess.Interfaces.UnitOfWork;

namespace MeetingService.Application.UseCases.Participants.Command.UpdateParticipantStatusCommand;

public class UpdateParticipantStatusHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateParticipantStatusCommand, ParticipantDto>
{
    public Task<ParticipantDto> Handle(UpdateParticipantStatusCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}