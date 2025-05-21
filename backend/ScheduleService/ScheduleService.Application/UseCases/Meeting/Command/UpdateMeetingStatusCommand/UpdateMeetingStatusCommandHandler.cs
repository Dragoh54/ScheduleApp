using Mapster;
using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.Application.Dto.Meetings.Responses;
using ScheduleService.Application.Interfaces.UnitOfWork;
using ScheduleService.DomainModel.Enums;
using ScheduleService.DomainModel.Exceptions;

namespace ScheduleService.Application.UseCases.Meeting.Command.UpdateMeetingStatusCommand;

public class UpdateMeetingStatusCommandHandler : IRequestHandler<UpdateMeetingStatusCommand, MeetingResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateMeetingStatusCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<MeetingResponseDto> Handle(UpdateMeetingStatusCommand request, CancellationToken cancellationToken)
    {
        var meeting = await _unitOfWork.Meetings.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException("Meeting not found");

        var isStatusInvalid = meeting.Status is MeetingStatus.Cancelled or MeetingStatus.Completed;
        if (isStatusInvalid)
        {
            throw new BadRequestException("This meeting is cancelled or completed");
        }
        
        await _unitOfWork.Meetings.UpdateMeetingStatusAsync(request.Id, request.Status, cancellationToken);
        
        var updatedMeeting = await _unitOfWork.Meetings.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException("Meeting not found");
        
        return updatedMeeting.Adapt<MeetingResponseDto>();
    }
}