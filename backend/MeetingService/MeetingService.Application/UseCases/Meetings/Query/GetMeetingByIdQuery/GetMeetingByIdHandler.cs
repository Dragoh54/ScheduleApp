using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.DataAccess.Interfaces.UnitOfWork;

namespace MeetingService.Application.UseCases.Meetings.Query.GetMeetingByIdQuery;

public class GetMeetingByIdHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetMeetingByIdQuery, MeetingDto>
{
    public Task<MeetingDto> Handle(GetMeetingByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}