using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.MeetingDtos;
using MeetingService.DomainModel.Enums;

namespace MeetingService.Application.UseCases.Meetings.Command.UpdateMeetingStatusCommand;

public record UpdateMeetingStatusCommand : IRequest<MeetingDto>
{
    public UpdateMeetingStatusCommand()
    {
    }
    
    public UpdateMeetingStatusCommand(Guid meetingId, UpdateMeetingStatusDto dto)
    {
        Id = meetingId;
        Status = dto.Status;
    }
    
    public Guid Id { get; set; }
    public MeetingStatus Status { get; set; }
}