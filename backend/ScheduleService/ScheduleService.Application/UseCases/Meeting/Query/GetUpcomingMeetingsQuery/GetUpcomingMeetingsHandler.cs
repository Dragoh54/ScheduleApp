using Mapster;
using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.DataAccess.Interfaces.UnitOfWork;
using ScheduleService.DataAccess.Repositories;
using ScheduleService.DomainModel.Exceptions;

namespace ScheduleService.Application.UseCases.Meeting.Query.GetUpcomingMeetingsQuery;

public class GetUpcomingMeetingsHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetUpcomingMeetingsQuery, IEnumerable<MeetingDto>>
{
    public async Task<IEnumerable<MeetingDto>> Handle(GetUpcomingMeetingsQuery request, CancellationToken cancellationToken)
    {
        var today = DateTime.Today;
        
        var meetings = await unitOfWork.Meetings.GetUserUpcomingMeetingsAsync(request.UserId, today, cancellationToken)
            ?? throw new NotFoundException("Meetings not found");

        return meetings.Adapt<IEnumerable<MeetingDto>>();
    }
}