using Mapster;
using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.ParticipantDtos;
using MeetingService.DataAccess.Interfaces.UnitOfWork;
using MeetingService.DomainModel.Exceptions;

namespace MeetingService.Application.UseCases.Participants.Query.GetParticipantsByMeetingIdQuery;

public class GetParticipantsByMeetingIdHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetParticipantsByMeetingIdQuery, IEnumerable<ParticipantDto>>
{
    public async Task<IEnumerable<ParticipantDto>> Handle(GetParticipantsByMeetingIdQuery request, CancellationToken cancellationToken)
    {
        var participants = await unitOfWork.ParticipantRepository.GetParticipantsByMeetingId(request.MeetingId, cancellationToken)
            ?? throw new NotFoundException("Meeting not found");
        
        cancellationToken.ThrowIfCancellationRequested();

        return participants.Adapt<IEnumerable<ParticipantDto>>();
    }
}