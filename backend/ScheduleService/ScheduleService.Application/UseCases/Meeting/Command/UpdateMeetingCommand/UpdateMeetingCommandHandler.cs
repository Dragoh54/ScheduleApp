using Mapster;
using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.Application.Dto.Meetings.Responses;
using ScheduleService.Application.Interfaces.UnitOfWork;
using ScheduleService.DomainModel.Exceptions;

namespace ScheduleService.Application.UseCases.Meeting.Command.UpdateMeetingCommand;

public class UpdateMeetingCommandHandler : IRequestHandler<UpdateMeetingCommand, MeetingResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateMeetingCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<MeetingResponseDto> Handle(UpdateMeetingCommand request, CancellationToken cancellationToken)
    {
        var meeting = await _unitOfWork.Meetings.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException("Meeting not found");
        
        request.Adapt(meeting);
        
        var updatedMeeting = await _unitOfWork.Meetings.UpdateAsync(meeting, cancellationToken)
            ?? throw new BadRequestException("Meeting not updated");
        
        var success = await _unitOfWork.Commit(cancellationToken);
        if (!success)
        {
            throw new BadRequestException("Failed to update meeting");
        }
        
        return updatedMeeting.Adapt<MeetingResponseDto>();
    }
}