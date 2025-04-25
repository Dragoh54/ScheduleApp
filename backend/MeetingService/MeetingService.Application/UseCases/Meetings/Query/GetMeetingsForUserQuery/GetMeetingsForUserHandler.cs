using Mapster;
using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.DataAccess.Interfaces.UnitOfWork;
using MeetingService.DomainModel.Exceptions;
using MeetingService.DomainModel.Models;

namespace MeetingService.Application.UseCases.Meetings.Query.GetMeetingsForUserQuery;

public class GetMeetingsForUserHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetMeetingsForUserQuery, IEnumerable<MeetingDto>>
{
    public async Task<IEnumerable<MeetingDto>> Handle(GetMeetingsForUserQuery request, CancellationToken cancellationToken)
    {
        var meetings = await unitOfWork.MeetingRepository.GetMeetingsForUser(request.OrganizerId, cancellationToken)
            ?? throw new NotFoundException("Meetings not found");
        
        cancellationToken.ThrowIfCancellationRequested();

        return meetings.Adapt<IEnumerable<MeetingDto>>();
    }
}