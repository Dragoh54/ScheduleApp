using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.DataAccess.Interfaces.UnitOfWork;

namespace MeetingService.Application.UseCases.Meetings.Query.GetMeetingWithParticipantsQuery;

public class GetMeetingsWithParticipantsHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetMeetingsWithParticipantsQuery, IEnumerable<MeetingDto>>
{
    public Task<IEnumerable<MeetingDto>> Handle(GetMeetingsWithParticipantsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}