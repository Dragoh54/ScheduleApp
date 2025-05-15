using Mapster;
using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.DataAccess.Interfaces.UnitOfWork;
using ScheduleService.DomainModel.Exceptions;

namespace ScheduleService.Application.UseCases.Meeting.Query.GetUserMeetings;

public class GetUserMeetingsQueryHandler : IRequestHandler<GetUserMeetingsQuery, IEnumerable<MeetingDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUserMeetingsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<MeetingDto>> Handle(GetUserMeetingsQuery request, CancellationToken cancellationToken)
    {
        var meetings = await _unitOfWork.Meetings.GetUserMeetingsAsync(request.UserId, cancellationToken)
            ?? throw new NotFoundException("Meetings not found");

        return meetings.Adapt<IEnumerable<MeetingDto>>();
    }
}