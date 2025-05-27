using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.MeetingDto.Requests;
using MeetingService.Application.Dtos.MeetingDto.Responses;
using MeetingService.DomainModel.Enums;

namespace MeetingService.Application.UseCases.Meetings.Command.UpdateMeetingStatusCommand;

public record UpdateMeetingStatusCommand : IRequest<MeetingWithParticipantsResponseDto>
{
    public UpdateMeetingStatusCommand()
    {
    }
    
    public UpdateMeetingStatusCommand(Guid meetingId, UpdateMeetingStatusRequestDto requestDto)
    {
        Id = meetingId;
        Status = requestDto.Status;
    }
    
    public Guid Id { get; set; }
    public MeetingStatus Status { get; set; }
}