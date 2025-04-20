using Mapster;
using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.DataAccess.Interfaces.UnitOfWork;
using ScheduleService.DomainModel.Exceptions;

namespace ScheduleService.Application.UseCases.Meeting.Query.GetUserMeetings;

public class GetUserMeetingsHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetUserMeetingsQuery, IEnumerable<MeetingDto>>
{
    public async Task<IEnumerable<MeetingDto>> Handle(GetUserMeetingsQuery request, CancellationToken cancellationToken)
    {
        var meetings = await unitOfWork.Meetings.GetUserMeetingsAsync(request.UserId, cancellationToken)
            ?? throw new NotFoundException("Meetings not found");

        return meetings.Adapt<IEnumerable<MeetingDto>>();
    }
}