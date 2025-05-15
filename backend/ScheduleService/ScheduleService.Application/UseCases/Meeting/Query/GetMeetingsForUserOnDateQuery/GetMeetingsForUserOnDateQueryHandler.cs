using Mapster;
using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.Application.Dto.Meetings.Responses;
using ScheduleService.DataAccess.Interfaces.UnitOfWork;
using ScheduleService.DomainModel.Exceptions;

namespace ScheduleService.Application.UseCases.Meeting.Query.GetMeetingsForUserOnDateQuery;

public class GetMeetingsForUserOnDateQueryHandler : IRequestHandler<GetMeetingsForUserOnDateQuery, IEnumerable<MeetingResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetMeetingsForUserOnDateQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<MeetingResponseDto>> Handle(GetMeetingsForUserOnDateQuery request, CancellationToken cancellationToken)
    {
        var meetings = await _unitOfWork.Meetings.GetMeetingsForUserOnDateAsync(request.UserId, request.Date, cancellationToken)
            ?? throw new NotFoundException("Meetings not found");

        return meetings.Adapt<IEnumerable<MeetingResponseDto>>();
    }
}