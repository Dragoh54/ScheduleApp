using Mapster;
using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.ParticipantDtos;
using MeetingService.DataAccess.Interfaces.UnitOfWork;
using MeetingService.DomainModel.Exceptions;

namespace MeetingService.Application.UseCases.Participants.Query.GetParticipantQuery;

public class GetParticipantHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetParticipantQuery, ParticipantWithMeetingDto>
{
    public async Task<ParticipantWithMeetingDto> Handle(GetParticipantQuery request, CancellationToken cancellationToken)
    {
        var participant = await unitOfWork.ParticipantRepository.GetParticipant(request.MeetingId, request.UserId, cancellationToken)
            ?? throw new NotFoundException("Meeting not found");
        
        cancellationToken.ThrowIfCancellationRequested();

        return participant.Adapt<ParticipantWithMeetingDto>();
    }
}