using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.DomainModel.Enums;

namespace MeetingService.Application.UseCases.Meetings.Command.UpdateMeetingStatusCommand;

public record UpdateMeetingStatusCommand : IRequest<MeetingDto>
{
    public Guid Id { get; set; }
    public MeetingStatus Status { get; set; }
}