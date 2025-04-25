using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.DataAccess.Interfaces.UnitOfWork;

namespace MeetingService.Application.UseCases.Meetings.Query.GetMeetingsInRangeQuery;

public class GetMeetingsInRangeHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetMeetingsInRangeQuery, IEnumerable<MeetingDto>>
{
    public Task<IEnumerable<MeetingDto>> Handle(GetMeetingsInRangeQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}