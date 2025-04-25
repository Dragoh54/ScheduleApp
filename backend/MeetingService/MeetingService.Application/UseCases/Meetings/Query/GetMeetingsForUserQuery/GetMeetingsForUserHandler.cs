using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.DataAccess.Interfaces.UnitOfWork;
using MeetingService.DomainModel.Models;

namespace MeetingService.Application.UseCases.Meetings.Query.GetMeetingsForUserQuery;

public class GetMeetingsForUserHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetMeetingsForUserQuery, IEnumerable<MeetingDto>>
{
    public Task<IEnumerable<MeetingDto>> Handle(GetMeetingsForUserQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}