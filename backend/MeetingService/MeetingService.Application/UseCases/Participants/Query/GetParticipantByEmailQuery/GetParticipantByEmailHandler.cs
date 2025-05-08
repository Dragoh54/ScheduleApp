using Mapster;
using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.ParticipantDtos;
using MeetingService.DataAccess.Interfaces.UnitOfWork;
using MeetingService.DomainModel.Exceptions;

namespace MeetingService.Application.UseCases.Participants.Query.GetParticipantByEmailQuery;

public class GetParticipantByEmailHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetParticipantByEmailQuery, ParticipantWithMeetingDto>
{
    public async Task<ParticipantWithMeetingDto> Handle(GetParticipantByEmailQuery request, CancellationToken cancellationToken)
    {
        var participant = unitOfWork.ParticipantRepository.GetParticipantByEmail(request.MeetingId, request.Email, cancellationToken)
            ?? throw new NotFoundException("Participant not found");
        
        cancellationToken.ThrowIfCancellationRequested();
        
        return participant.Adapt<ParticipantWithMeetingDto>();
    }
}