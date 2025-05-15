using Mapster;
using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.Application.Dto.Meetings.Responses;
using ScheduleService.Application.Interfaces.UnitOfWork;
using ScheduleService.DomainModel.Exceptions;

namespace ScheduleService.Application.UseCases.Meeting.Query.GetMeetingByIdQuery;

public class GetMeetingByIdQueryHandler : IRequestHandler<GetMeetingByIdQuery, MeetingResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetMeetingByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<MeetingResponseDto> Handle(GetMeetingByIdQuery request, CancellationToken cancellationToken)
    {
        var meeting = await _unitOfWork.Meetings.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException("Meeting not found");

        return meeting.Adapt<MeetingResponseDto>();
    }
}