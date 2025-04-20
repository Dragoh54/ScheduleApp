using Mapster;
using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.DataAccess.Interfaces.UnitOfWork;
using ScheduleService.DomainModel.Exceptions;

namespace ScheduleService.Application.UseCases.Meeting.Query.GetMeetingsForUserOnDateQuery;

public class GetMeetingsForUserOnDateHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetMeetingsForUserOnDateQuery, IEnumerable<MeetingDto>>
{
    public async Task<IEnumerable<MeetingDto>> Handle(GetMeetingsForUserOnDateQuery request, CancellationToken cancellationToken)
    {
        var meetings = await unitOfWork.Meetings.GetMeetingsForUserOnDateAsync(request.UserId, request.Date, cancellationToken)
            ?? throw new NotFoundException("Meetings not found");

        return meetings.Adapt<IEnumerable<MeetingDto>>();
    }
}