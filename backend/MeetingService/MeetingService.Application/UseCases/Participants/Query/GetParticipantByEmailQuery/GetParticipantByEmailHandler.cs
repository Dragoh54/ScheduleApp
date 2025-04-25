using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.DataAccess.Interfaces.UnitOfWork;

namespace MeetingService.Application.UseCases.Participants.Query.GetParticipantByEmailQuery;

public class GetParticipantByEmailHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetParticipantByEmailQuery, ParticipantDto>
{
    public Task<ParticipantDto> Handle(GetParticipantByEmailQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}