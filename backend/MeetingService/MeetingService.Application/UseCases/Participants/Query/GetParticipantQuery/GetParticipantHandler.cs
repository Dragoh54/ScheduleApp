using Mapster;
using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.DataAccess.Interfaces.UnitOfWork;
using MeetingService.DomainModel.Exceptions;

namespace MeetingService.Application.UseCases.Participants.Query.GetParticipantQuery;

public class GetParticipantHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetParticipantQuery, ParticipantDto>
{
    public async Task<ParticipantDto> Handle(GetParticipantQuery request, CancellationToken cancellationToken)
    {
        var participant = await unitOfWork.ParticipantRepository.GetParticipant(request.MeetingId, request.UserId, cancellationToken)
            ?? throw new NotFoundException("Meeting not found");
        
        cancellationToken.ThrowIfCancellationRequested();

        return participant.Adapt<ParticipantDto>();
    }
}