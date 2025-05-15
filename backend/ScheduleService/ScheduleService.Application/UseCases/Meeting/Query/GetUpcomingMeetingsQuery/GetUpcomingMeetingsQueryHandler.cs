using Mapster;
using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.Application.Dto.Meetings.Responses;
using ScheduleService.DataAccess.Interfaces.UnitOfWork;
using ScheduleService.DataAccess.Repositories;
using ScheduleService.DomainModel.Exceptions;

namespace ScheduleService.Application.UseCases.Meeting.Query.GetUpcomingMeetingsQuery;

public class GetUpcomingMeetingsQueryHandler : IRequestHandler<GetUpcomingMeetingsQuery, IEnumerable<MeetingResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUpcomingMeetingsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<MeetingResponseDto>> Handle(GetUpcomingMeetingsQuery request, CancellationToken cancellationToken)
    {
        var today = DateTime.Today;
        
        var meetings = await _unitOfWork.Meetings.GetUserUpcomingMeetingsAsync(request.UserId, today, cancellationToken)
            ?? throw new NotFoundException("Meetings not found");

        return meetings.Adapt<IEnumerable<MeetingResponseDto>>();
    }
}