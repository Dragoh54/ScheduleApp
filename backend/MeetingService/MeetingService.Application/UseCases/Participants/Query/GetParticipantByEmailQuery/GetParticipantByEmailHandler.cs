using Mapster;
using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.DataAccess.Interfaces.UnitOfWork;
using MeetingService.DomainModel.Exceptions;

namespace MeetingService.Application.UseCases.Participants.Query.GetParticipantByEmailQuery;

public class GetParticipantByEmailHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetParticipantByEmailQuery, ParticipantDto>
{
    public async Task<ParticipantDto> Handle(GetParticipantByEmailQuery request, CancellationToken cancellationToken)
    {
        var participant = unitOfWork.ParticipantRepository.GetParticipantByEmail(request.MeetingId, request.Email, cancellationToken)
            ?? throw new NotFoundException("Participant not found");
        
        cancellationToken.ThrowIfCancellationRequested();
        
        return participant.Adapt<ParticipantDto>();
    }
}