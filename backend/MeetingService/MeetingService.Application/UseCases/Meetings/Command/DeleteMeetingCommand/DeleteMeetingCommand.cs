using MediatR;
using MeetingService.Application.Dtos;

namespace MeetingService.Application.UseCases.Meetings.Command.DeleteMeetingCommand;

public record DeleteMeetingCommand : IRequest<MeetingDto>
{
    public Guid Id { get; set; }
}