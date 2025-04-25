using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.DataAccess.Interfaces.UnitOfWork;

namespace MeetingService.Application.UseCases.Participants.Query.GetParticipantByMeetingIdAndUserIdQuery;

public class GetParticipantByMeetingIdAndUserIdHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetParticipantByMeetingIdAndUserIdQuery, ParticipantDto>
{
    public Task<ParticipantDto> Handle(GetParticipantByMeetingIdAndUserIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}