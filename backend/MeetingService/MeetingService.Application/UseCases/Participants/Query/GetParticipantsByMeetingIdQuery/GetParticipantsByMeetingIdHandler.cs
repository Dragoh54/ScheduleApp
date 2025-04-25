using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.DataAccess.Interfaces.UnitOfWork;

namespace MeetingService.Application.UseCases.Participants.Query.GetParticipantsByMeetingIdQuery;

public class GetParticipantsByMeetingIdHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetParticipantsByMeetingIdQuery, IEnumerable<ParticipantDto>>
{
    public Task<IEnumerable<ParticipantDto>> Handle(GetParticipantsByMeetingIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}