using Mapster;
using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.Application.Dto.Meetings.Responses;
using ScheduleService.Application.Interfaces.UnitOfWork;
using ScheduleService.DomainModel.Enums;
using ScheduleService.DomainModel.Exceptions;

namespace ScheduleService.Application.UseCases.Meeting.Command.CreateMeetingCommand;

public class CreateMeetingCommandHandler : IRequestHandler<CreateMeetingCommand, MeetingResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateMeetingCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<MeetingResponseDto> Handle(CreateMeetingCommand request, CancellationToken cancellationToken)
    {
        var meeting = await _unitOfWork.Meetings.AddAsync(request.Adapt<DomainModel.Models.Meeting>(), cancellationToken);
        
        return meeting.Adapt<MeetingResponseDto>();
    }
    
}