using Mapster;
using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.DataAccess.Interfaces.UnitOfWork;
using ScheduleService.DomainModel.Exceptions;

namespace ScheduleService.Application.UseCases.Meeting.Query.GetMeetingsForUserInRangeQuery;

public class GetMeetingsForUserInRangeHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetMeetingsForUserInRangeQuery, IEnumerable<MeetingDto>>
{
    public async Task<IEnumerable<MeetingDto>> Handle(GetMeetingsForUserInRangeQuery request, CancellationToken cancellationToken)
    {
        var meetings = await unitOfWork.Meetings.GetMeetingsForUserInRangeAsync(request.UserId, request.StartDate, request.EndDate, cancellationToken)
            ?? throw new NotFoundException("Meetings not found");

        return meetings.Adapt<IEnumerable<MeetingDto>>();
    }
}