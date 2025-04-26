using Mapster;
using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.MeetingDtos;
using MeetingService.DataAccess.Interfaces.UnitOfWork;
using MeetingService.DomainModel.Exceptions;

namespace MeetingService.Application.UseCases.Meetings.Query.GetMeetingsOrganizedByUserQuery;

public class GetMeetingsOrganizedByUserHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetMeetingsOrganizedByUserQuery, IEnumerable<MeetingDto>>
{
    public async Task<IEnumerable<MeetingDto>> Handle(GetMeetingsOrganizedByUserQuery request, CancellationToken cancellationToken)
    {
        var meetings = await unitOfWork.MeetingRepository.GetMeetingsForUser(request.OrganizerId, cancellationToken)
            ?? throw new NotFoundException("Meetings not found");
        
        cancellationToken.ThrowIfCancellationRequested();

        return meetings.Adapt<IEnumerable<MeetingDto>>();
    }
}