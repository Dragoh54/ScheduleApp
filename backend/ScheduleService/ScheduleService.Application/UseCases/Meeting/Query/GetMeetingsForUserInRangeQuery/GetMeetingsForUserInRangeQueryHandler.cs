using Mapster;
using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.Application.Dto.Meetings.Responses;
using ScheduleService.Application.Interfaces.UnitOfWork;
using ScheduleService.DomainModel.Exceptions;

namespace ScheduleService.Application.UseCases.Meeting.Query.GetMeetingsForUserInRangeQuery;

public class GetMeetingsForUserInRangeQueryHandler : IRequestHandler<GetMeetingsForUserInRangeQuery, IEnumerable<MeetingResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetMeetingsForUserInRangeQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<MeetingResponseDto>> Handle(GetMeetingsForUserInRangeQuery request, CancellationToken cancellationToken)
    {
        var meetings = await _unitOfWork.Meetings.GetMeetingsForUserInRangeAsync(request.UserId, request.StartDate, request.EndDate, cancellationToken)
            ?? throw new NotFoundException("Meetings not found");

        return meetings.Adapt<IEnumerable<MeetingResponseDto>>();
    }
}