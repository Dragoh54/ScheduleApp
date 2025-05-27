using Mapster;
using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.Application.Dto.Meetings.Responses;
using ScheduleService.Application.Interfaces.UnitOfWork;
using ScheduleService.DomainModel.Exceptions;

namespace ScheduleService.Application.UseCases.Meeting.Query.GetUserMeetings;

public class GetUserMeetingsQueryHandler : IRequestHandler<GetUserMeetingsQuery, IEnumerable<MeetingResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUserMeetingsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<MeetingResponseDto>> Handle(GetUserMeetingsQuery request, CancellationToken cancellationToken)
    {
        var meetings = await _unitOfWork.Meetings.GetUserMeetingsAsync(request.UserId, cancellationToken)
            ?? throw new NotFoundException("Meetings not found");

        return meetings.Adapt<IEnumerable<MeetingResponseDto>>();
    }
}