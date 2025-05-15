using Mapster;
using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.DataAccess.Interfaces.UnitOfWork;
using ScheduleService.DataAccess.Repositories;
using ScheduleService.DomainModel.Exceptions;

namespace ScheduleService.Application.UseCases.Meeting.Query.GetMeetingByIdQuery;

public class GetMeetingByIdQueryHandler : IRequestHandler<GetMeetingByIdQuery, MeetingDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetMeetingByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<MeetingDto> Handle(GetMeetingByIdQuery request, CancellationToken cancellationToken)
    {
        var meeting = await _unitOfWork.Meetings.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException("Meeting not found");

        return meeting.Adapt<MeetingDto>();
    }
}